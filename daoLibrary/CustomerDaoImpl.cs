using entityLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using utilLibrary;

namespace daoLibrary
{
    public class CustomerDaoImpl : ICustomerDao
    {
        private readonly string connectionString;

        // Constructor that accepts a connection string
        public CustomerDaoImpl(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Customer (CustomerID, Name, EmailAddress, PhoneNumber, Address, CreditScore) " +
                               "VALUES (@CustomerID, @Name, @EmailAddress, @PhoneNumber, @Address, @CreditScore)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@CreditScore", customer.CreditScore);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Customer GetCustomerById(int id)
        {
            Customer customer = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Customer WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerID", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer = new Customer
                    {
                        CustomerID = (int)reader["CustomerID"],
                        Name = (string)reader["Name"],
                        EmailAddress = (string)reader["EmailAddress"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        Address = (string)reader["Address"],
                        CreditScore = (int)reader["CreditScore"]
                    };
                }
            }

            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Customer";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        CustomerID = (int)reader["CustomerID"],
                        Name = (string)reader["Name"],
                        EmailAddress = (string)reader["EmailAddress"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        Address = (string)reader["Address"],
                        CreditScore = (int)reader["CreditScore"]
                    };
                    customers.Add(customer);
                }
            }

            return customers;
        }

        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Customer SET Name = @Name, EmailAddress = @EmailAddress, PhoneNumber = @PhoneNumber, " +
                               "Address = @Address, CreditScore = @CreditScore WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@CreditScore", customer.CreditScore);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Customer WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
