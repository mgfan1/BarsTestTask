using System;
using System.Data;
using System.Data.SqlClient;

namespace Bars.Data.DataAccess
{
    public class Procedure : IDisposable
    {
        #region Fields

        private const string ConnectionIsNullExMessage = "Connection is null";
        private const string TransactionIsNullExMessage = "Transaction is null";
        private const string ReaderIsNullExMessage = "Reader is null";
        private const int Timeout = 4 * 60 * 60;
        private readonly bool _isSharedConnection;
        private readonly SqlCommand _command;
        private readonly SqlConnection _connection;

        #endregion

        #region Constructors

        public Procedure(string procName)
        {
            _isSharedConnection = false;
            _connection = ConnectionManager.CreateDefaultConnection();
            _command = new SqlCommand(procName, _connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = Timeout
            };
        }

        public Procedure(SqlConnection connection, SqlTransaction transaction, string procName)
        {
            if (transaction == null)
                throw new ArgumentNullException(TransactionIsNullExMessage);

            _isSharedConnection = true;
            _connection = connection ?? throw new ArgumentNullException(ConnectionIsNullExMessage);
            _command = new SqlCommand(procName, _connection, transaction)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = Timeout
            };
        }

        public Procedure(SqlConnection conn, string procName)
        {
            _isSharedConnection = true;
            _connection = conn ?? throw new ArgumentNullException(ConnectionIsNullExMessage);
            _command = new SqlCommand(procName, _connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = Timeout
            };
        }

        #endregion

        #region Methods

        public virtual T ExecuteReader<T>(Func<SqlDataReader, T> action)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                using var reader = _command.ExecuteReader();
                if (null == reader)
                    throw new NullReferenceException(ReaderIsNullExMessage);

                return action(reader);
            }
            catch (SqlException)
            {
                // todo Add logging
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Procedure()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_isSharedConnection)
                {
                    _connection.Dispose();
                }
                _command.Dispose();
            }
        }

        #endregion
    }
}