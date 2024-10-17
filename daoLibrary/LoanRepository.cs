// LoanRepository.cs (in dao)
using entityLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using exception; // Add this if it's missing


namespace daoLibrary
{
    public class LoanRepository : ILoanRepository
    {
        private readonly string _connectionString;

        public LoanRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ApplyLoan(Loan loan)
        {
            Console.Write("Do you want to apply for this loan? (Yes/No): ");
            string confirmation = Console.ReadLine();

            if (confirmation?.Trim().ToLower() == "yes")
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // SQL command to insert loan into database
                    string query = "INSERT INTO Loans (CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) VALUES (@CustomerId, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, @LoanStatus)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerID);
                        command.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                        command.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                        command.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                        command.Parameters.AddWithValue("@LoanType", loan.LoanType);
                        command.Parameters.AddWithValue("@LoanStatus", "Pending");

                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Loan applied successfully.");
                }
            }
            else
            {
                Console.WriteLine("Loan application cancelled.");
            }
        }

        public decimal CalculateInterest(int loanId)
        {
            Loan loan = GetLoanById(loanId); // Method to retrieve loan from database

            if (loan == null)
            {
                throw new InvalidLoanException("Loan not found.");
            }

            return CalculateInterest(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public decimal CalculateInterest(decimal principalAmount, decimal interestRate, int loanTerm)
        {
            // Interest = (Principal Amount * Interest Rate * Loan Tenure) / 12
            return (principalAmount * interestRate * loanTerm) / 12;
        }

        public void LoanStatus(int loanId)
        {
            Loan loan = GetLoanById(loanId); // Method to retrieve loan from database

            if (loan == null)
            {
                throw new InvalidLoanException("Loan not found.");
            }

            if (loan.Customer.CreditScore > 650)
            {
                UpdateLoanStatus(loanId, "Approved");
                Console.WriteLine("Loan approved.");
            }
            else
            {
                UpdateLoanStatus(loanId, "Rejected");
                Console.WriteLine("Loan rejected due to low credit score.");
            }
        }

        public decimal CalculateEMI(int loanId)
        {
            Loan loan = GetLoanById(loanId); // Method to retrieve loan from database

            if (loan == null)
            {
                throw new InvalidLoanException("Loan not found.");
            }

            return CalculateEMI(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public decimal CalculateEMI(decimal principalAmount, decimal interestRate, int loanTerm)
        {
            // EMI = [P * R * (1+R)^N] / [(1+R)^N-1]
            decimal r = interestRate / 12 / 100; // Monthly interest rate
            return (principalAmount * r * (decimal)Math.Pow((double)(1 + r), loanTerm)) / ((decimal)Math.Pow((double)(1 + r), loanTerm) - 1);
        }

        public void LoanRepayment(int loanId, decimal amount)
        {
            Loan loan = GetLoanById(loanId); // Retrieve the loan details

            if (loan == null)
            {
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");
            }

            decimal emi = CalculateEMI(loanId); // Calculate the EMI for the loan

            if (amount < emi)
            {
                Console.WriteLine("Payment rejected: The amount is less than the EMI.");
                return;
            }

            // Update the loan balance or other related logic
            // Example: Deduct amount from loan balance and update database
            // Here you would typically update the database with the new loan balance
            // and/or payment record

            Console.WriteLine($"Payment of {amount} accepted for Loan ID {loanId}.");
        }

        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = new List<Loan>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Loans", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Create a Loan object from the database fields
                    Loan loan = new Loan
                    {
                        LoanId = (int)reader["LoanId"],
                        // Populate other properties accordingly
                        // Example: PrincipalAmount = (decimal)reader["PrincipalAmount"],
                        // Continue for other properties...
                    };

                    loans.Add(loan);
                }
            }

            return loans;
        }

        public Loan GetLoanById(int loanId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Loans WHERE LoanId = @LoanId", connection);
                command.Parameters.AddWithValue("@LoanId", loanId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Create and return the Loan object
                    return new Loan
                    {
                        LoanId = (int)reader["LoanId"],
                        // Populate other properties accordingly
                        // Example: PrincipalAmount = (decimal)reader["PrincipalAmount"],
                        // Continue for other properties...
                    };
                }
                else
                {
                    throw new InvalidLoanException($"Loan with ID {loanId} not found.");
                }
            }
        }

        private void UpdateLoanStatus(int loanId, string status)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Loans SET LoanStatus = @Status WHERE LoanId = @LoanId", connection);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@LoanId", loanId);
                command.ExecuteNonQuery();
            }
        }
    }
}

