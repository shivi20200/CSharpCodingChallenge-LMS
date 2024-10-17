// DBUtil.cs (in util)
using System;
using System.Data.SqlClient;
using System.IO;

namespace utilLibrary
{
    public static class DBUtil
    {
        // Method to get database connection
        public static SqlConnection GetDBConn()
        {
            string connectionString = GetConnectionString("DBConfig.properties");
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open(); // Establish the connection
                return connection; // Return the open connection
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error establishing connection: {ex.Message}");
                throw; // Rethrow the exception for further handling
            }
        }

        // Method to retrieve the connection string from a properties file
        private static string GetConnectionString(string propertyFileName)
        {
            // Assuming the properties file is structured as key-value pairs
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, propertyFileName);
            var properties = new System.Collections.Generic.Dictionary<string, string>();

            foreach (var line in File.ReadLines(path))
            {
                var keyValue = line.Split('=');
                if (keyValue.Length == 2)
                {
                    properties[keyValue[0].Trim()] = keyValue[1].Trim();
                }
            }

            // Return the connection string from the properties file
            if (properties.TryGetValue("ConnectionString", out string connString))
            {
                return connString;
            }

            throw new Exception("Connection string not found in the properties file.");
        }
    }
}
