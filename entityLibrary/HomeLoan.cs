// HomeLoan.cs
using entityLibrary;

public class HomeLoan : Loan
{
    public string PropertyAddress { get; set; }
    public decimal PropertyValue { get; set; } // Change to decimal

    // Default constructor
    public HomeLoan() { }

    // Overloaded constructor
    public HomeLoan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm, string propertyAddress, decimal propertyValue) // Change to decimal
        : base(loanId, customer, principalAmount, interestRate, loanTerm, "HomeLoan", "Pending") // Call base constructor
    {
        PropertyAddress = propertyAddress;
        PropertyValue = propertyValue;
    }

    // Method to print all information
    public void PrintHomeLoanInfo()
    {
        PrintLoanInfo(); // Print common loan info
        Console.WriteLine($"Property Address: {PropertyAddress}");
        Console.WriteLine($"Property Value: {PropertyValue}");
    }
}
