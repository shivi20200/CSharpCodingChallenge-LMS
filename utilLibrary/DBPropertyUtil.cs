namespace utilLibrary
{
    public static class DBPropertyUtil
    {
        public static string GetConnectionString(string fileName)
        {
            // Return the connection string directly
            return @"Data Source=.\\sqlexpress;Initial Catalog=LoanManagementSystem;Integrated Security=True;Trust Server Certificate=True";
        }
    }
}


