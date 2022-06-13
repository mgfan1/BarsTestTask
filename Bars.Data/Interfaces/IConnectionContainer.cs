using System;
using System.Data.SqlClient;

namespace Bars.Data.Interfaces
{
    public interface IConnectionContainer : IDAComponent, IDisposable
    {
        public SqlConnection Connection { get; }
        public SqlTransaction CurrentTransaction { get; }

        public bool IsExistsForCurrentThread();
        public IConnectionContainer ForCurrentThread(bool alwaysCreateNew = false);
    }
}