using System.Data.SqlClient;

namespace utilLibrary
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
