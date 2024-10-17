// CarLoan.cs (in entityLibrary)
namespace entityLibrary
{
    public class CarLoan : Loan
    {
        public string CarModel { get; set; }
        public decimal CarValue { get; set; }

        // Default constructor
        public CarLoan() { }

        // Overloaded constructor
        public CarLoan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm, string carModel, decimal carValue)
            : base(loanId, customer, principalAmount, interestRate, loanTerm, "CarLoan", "Pending") // Call base constructor
        {
            CarModel = carModel;
            CarValue = carValue;
        }

        // Method to print all information
        public void PrintCarLoanInfo()
        {
            PrintLoanInfo(); // Print common loan info
            Console.WriteLine($"Car Model: {CarModel}");
            Console.WriteLine($"Car Value: {CarValue}");
        }
    }
}
