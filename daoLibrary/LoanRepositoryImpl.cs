using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using entityLibrary;
using exception; // Assuming your custom exceptions are in this namespace

namespace daoLibrary
{
    public class LoanRepositoryImpl : ILoanRepository
    {
        private string connectionString;

        public LoanRepositoryImpl(string connString)
        {
            connectionString = connString;
        }

        public void ApplyLoan(Loan loan)
        {
            // Get user confirmation (mocked for simplicity)
            Console.WriteLine("Do you want to apply for the loan? (Yes/No)");
            string confirmation = Console.ReadLine();

            if (confirmation?.ToLower() == "yes")
            {
                loan.LoanStatus = "Pending"; // Set initial status

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Loans (CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) VALUES (@CustomerId, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, @LoanStatus)", connection);
                    command.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerID);
                    command.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                    command.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                    command.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                    command.Parameters.AddWithValue("@LoanType", loan.LoanType);
                    command.Parameters.AddWithValue("@LoanStatus", loan.LoanStatus);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                Console.WriteLine("Loan application canceled.");
            }
        }

        public decimal CalculateInterest(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            if (loan == null)
            {
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");
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
            Loan loan = GetLoanById(loanId);
            if (loan == null)
            {
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");
            }

            if (loan.Customer.CreditScore > 650)
            {
                loan.LoanStatus = "Approved";
                UpdateLoanStatusInDatabase(loanId, loan.LoanStatus);
                Console.WriteLine("Loan approved.");
            }
            else
            {
                loan.LoanStatus = "Rejected";
                UpdateLoanStatusInDatabase(loanId, loan.LoanStatus);
                Console.WriteLine("Loan rejected due to low credit score.");
            }
        }

        public decimal CalculateEMI(int loanId) // Change void to decimal
        {
            Loan loan = GetLoanById(loanId);
            if (loan == null)
            {
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");
            }

            // Calculate and return the EMI
            return CalculateEMI(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public decimal CalculateEMI(decimal principalAmount, decimal interestRate, int loanTerm)
        {
            // EMI = [P * R * (1 + R)^N] / [(1 + R)^N - 1]
            decimal monthlyRate = interestRate / 12 / 100; // Convert annual interest rate to monthly

            // Calculate the power term separately
            decimal powerTerm = (decimal)Math.Pow((double)(1 + monthlyRate), loanTerm);

            // EMI calculation
            decimal emi = (principalAmount * monthlyRate * powerTerm) / (powerTerm - 1);

            return emi;
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

            // Logic to process the repayment in the database
            // e.g., deduct amount from loan balance, update database, etc.
            Console.WriteLine($"Payment of {amount} accepted for Loan ID {loanId}.");
        }

        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = new List<Loan>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Loans", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Loan loan = new Loan
                    {
                        LoanId = (int)reader["LoanId"],
                        // Populate other properties here
                    };
                    loans.Add(loan);
                }
            }

            return loans;
        }

        public Loan GetLoanById(int loanId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Loans WHERE LoanId = @LoanId", connection);
                command.Parameters.AddWithValue("@LoanId", loanId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Loan
                    {
                        LoanId = (int)reader["LoanId"],
                        // Populate other properties here
                    };
                }
                else
                {
                    throw new InvalidLoanException($"Loan with ID {loanId} not found.");
                }
            }
        }

        private void UpdateLoanStatusInDatabase(int loanId, string status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Loans SET LoanStatus = @LoanStatus WHERE LoanId = @LoanId", connection);
                command.Parameters.AddWithValue("@LoanStatus", status);
                command.Parameters.AddWithValue("@LoanId", loanId);
                command.ExecuteNonQuery();
            }
        }
    }
}


