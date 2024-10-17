// Loan.cs (in entityLibrary)
namespace entityLibrary
{
    public class Loan
    {
        public int LoanId { get; set; }
        public Customer Customer { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int LoanTerm { get; set; } // Loan tenure in months
        public string LoanType { get; set; } // "CarLoan" or "HomeLoan"
        public string LoanStatus { get; set; } // "Pending" or "Approved"

        // Default constructor
        public Loan() { }

        // Overloaded constructor
        public Loan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm, string loanType, string loanStatus)
        {
            LoanId = loanId;
            Customer = customer;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
            LoanStatus = loanStatus;
        }

        // Method to print all information
        public void PrintLoanInfo()
        {
            Console.WriteLine($"Loan ID: {LoanId}");
            Console.WriteLine($"Customer: {Customer.Name}");
            Console.WriteLine($"Principal Amount: {PrincipalAmount}");
            Console.WriteLine($"Interest Rate: {InterestRate}");
            Console.WriteLine($"Loan Term: {LoanTerm} months");
            Console.WriteLine($"Loan Type: {LoanType}");
            Console.WriteLine($"Loan Status: {LoanStatus}");
        }
    }
}
