using System.Data.SqlClient;

namespace Bars.Data.DataAccess
{
    public static class ConnectionManager
    {
        public static SqlConnection CreateDefaultConnection()
        {
            return new SqlConnection(Properties.Resources.BarsConnectionString);
        }
    }
}