using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using Bars.Data.Interfaces;

namespace Bars.Data.DataAccess
{
    internal class ConnectionContainer : IConnectionContainer
    {
        public SqlConnection Connection { get; protected set; }
        public SqlTransaction CurrentTransaction { get; protected set; }
        private readonly object _cacheLockValue = new object();
        private readonly Dictionary<int, Stack<IConnectionContainer>> _cacheValue;
        private readonly ConnectionContainer _parentValue;

        #region Methods

        public ConnectionContainer()
        {
            _cacheValue = new Dictionary<int, Stack<IConnectionContainer>>();
        }

        protected ConnectionContainer(ConnectionContainer parentContainer)
        {
            _parentValue = parentContainer;
        }

        protected void RemoveConnectionFromCache(IConnectionContainer container)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            lock (_cacheLockValue)
            {
                if (!_cacheValue.ContainsKey(threadId))
                    return;

                var stack = _cacheValue[threadId];
                if (0 == stack.Count)
                    return;

                if (stack.Peek() == container)
                    stack.Pop();
            }
        }

        protected virtual void CreateConnection()
        {
            Connection = ConnectionManager.CreateDefaultConnection();
            Connection.Open();
        }

        private void RollbackSafely()
        {
            if (null == CurrentTransaction)
                return;

            CurrentTransaction.Rollback();
            CurrentTransaction = null;
        }

        public IConnectionContainer ForCurrentThread(bool alwaysCreateNew = false)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            Stack<IConnectionContainer> stack;

            lock (_cacheLockValue)
            {
                if (!_cacheValue.ContainsKey(threadId))
                    _cacheValue.Add(threadId, new Stack<IConnectionContainer>());

                stack = _cacheValue[threadId];
            }

            if (alwaysCreateNew || (0 == stack.Count))
            {
                var newInstance = new ConnectionContainer(this);
                newInstance.CreateConnection();
                stack.Push(newInstance);
            }

            return stack.Peek();
        }

        public bool IsExistsForCurrentThread()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            Stack<IConnectionContainer> stack;

            lock (_cacheLockValue)
            {
                if (!_cacheValue.ContainsKey(threadId))
                    return false;

                stack = _cacheValue[threadId];
            }

            return 0 != stack.Count;
        }

        public void Dispose()
        {
            using (Connection)
            {
                RollbackSafely();

                if (null != _parentValue)
                    _parentValue.RemoveConnectionFromCache(this);
            }
        }

        #endregion
    }
}