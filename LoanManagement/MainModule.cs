using System;
using System.Collections.Generic;
using daoLibrary;
using entityLibrary;
using exception;
using utilLibrary;

namespace LoanManagement
{
    class MainModule
    {
        static void Main(string[] args)
        {
            string connectionString = DBPropertyUtil.GetConnectionString("dbconfig.properties");
            ICustomerDao customerDao = new CustomerDaoImpl(connectionString);
            ILoanRepository loanRepository = new LoanRepository("YourConnectionStringHere"); // Provide your actual connection string
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("=== Loan Management System ===");
                Console.WriteLine("1. Apply for a Loan");
                Console.WriteLine("2. Get All Loans");
                Console.WriteLine("3. Get Loan by ID");
                Console.WriteLine("4. Loan Repayment");
                Console.WriteLine("5. Add Customer");
                Console.WriteLine("6. View Customer");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ApplyForLoan(loanRepository);
                        break;
                    case "2":
                        GetAllLoans(loanRepository);
                        break;
                    case "3":
                        GetLoanById(loanRepository);
                        break;
                    case "4":
                        LoanRepayment(loanRepository);
                        break;
                    case "5":
                        AddCustomer(customerDao);
                        break;
                    case "6":
                        ViewCustomer(customerDao);
                        break;
                    case "7":
                        exit = true;
                        Console.WriteLine("Exiting the Loan Management System.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // Existing loan application methods (unchanged)
        private static void ApplyForLoan(ILoanRepository loanRepository)
        {
            Console.WriteLine("=== Apply for a Loan ===");
            // Collect customer details
            Console.Write("Enter Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Email Address: ");
            string email = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            string phone = Console.ReadLine();
            Console.Write("Enter Address: ");
            string address = Console.ReadLine();
            Console.Write("Enter Credit Score: ");
            int creditScore = int.Parse(Console.ReadLine());

            // Create a customer object
            Customer customer = new Customer(customerId, name, email, phone, address, creditScore);

            // Collect loan details
            Console.Write("Enter Principal Amount: ");
            decimal principalAmount = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Interest Rate: ");
            decimal interestRate = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Loan Term (in months): ");
            int loanTerm = int.Parse(Console.ReadLine());
            Console.Write("Enter Property Address (for HomeLoan) or Car Model (for CarLoan): ");
            string propertyOrCar = Console.ReadLine();

            // Determine loan type and create the appropriate loan object
            Loan loan;
            Console.Write("Enter Loan Type (Home/Car): ");
            string loanType = Console.ReadLine().ToLower();

            if (loanType == "home")
            {
                loan = new HomeLoan(0, customer, principalAmount, interestRate, loanTerm, propertyOrCar, principalAmount);
            }
            else if (loanType == "car")
            {
                loan = new CarLoan(0, customer, principalAmount, interestRate, loanTerm, propertyOrCar, principalAmount);
            }
            else
            {
                Console.WriteLine("Invalid loan type. Loan application cancelled.");
                return;
            }

            // Call the ApplyLoan method from the repository
            loanRepository.ApplyLoan(loan);
            Console.WriteLine("Loan application submitted successfully.");
        }

        private static void GetAllLoans(ILoanRepository loanRepository)
        {
            Console.WriteLine("=== Get All Loans ===");
            List<Loan> loans = loanRepository.GetAllLoans();

            foreach (var loan in loans)
            {
                Console.WriteLine($"Loan ID: {loan.LoanId}, Status: {loan.LoanStatus}, Amount: {loan.PrincipalAmount}");
            }
        }

        private static void GetLoanById(ILoanRepository loanRepository)
        {
            Console.WriteLine("=== Get Loan by ID ===");
            Console.Write("Enter Loan ID: ");
            int loanId = int.Parse(Console.ReadLine());

            try
            {
                Loan loan = loanRepository.GetLoanById(loanId);
                Console.WriteLine($"Loan ID: {loan.LoanId}, Status: {loan.LoanStatus}, Amount: {loan.PrincipalAmount}");
            }
            catch (InvalidLoanException)
            {
                Console.WriteLine("Loan not found.");
            }
        }

        private static void LoanRepayment(ILoanRepository loanRepository)
        {
            Console.WriteLine("=== Loan Repayment ===");
            Console.Write("Enter Loan ID: ");
            int loanId = int.Parse(Console.ReadLine());
            Console.Write("Enter repayment amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            loanRepository.LoanRepayment(loanId, amount);
            Console.WriteLine("Loan repayment processed successfully.");
        }

        // Customer management methods
        private static void AddCustomer(ICustomerDao customerDao)
        {
            Console.WriteLine("=== Add Customer ===");
            Customer newCustomer = new Customer
            {
                CustomerID = 1, // You may want to implement a way to generate unique IDs
                Name = "John Doe",
                EmailAddress = "john@example.com",
                PhoneNumber = "123-456-7890",
                Address = "123 Main St",
                CreditScore = 750
            };
            customerDao.AddCustomer(newCustomer);
            Console.WriteLine("Customer added successfully.");
        }

        private static void ViewCustomer(ICustomerDao customerDao)
        {
            Console.WriteLine("=== View Customer ===");
            Console.Write("Enter Customer ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Customer customer = customerDao.GetCustomerById(id);
            if (customer != null)
            {
                Console.WriteLine($"Customer Name: {customer.Name}, Email: {customer.EmailAddress}");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
    }
}
