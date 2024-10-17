// ILoanRepository.cs (in dao)
using entityLibrary;
using System;
using System.Collections.Generic;

namespace daoLibrary
{
    public interface ILoanRepository
    {
        // Method to apply for a loan
        void ApplyLoan(Loan loan);

        // Method to calculate interest by loanId
        decimal CalculateInterest(int loanId);

        // Overloaded method to calculate interest with parameters
        decimal CalculateInterest(decimal principalAmount, decimal interestRate, int loanTerm);

        // Method to check loan status
        void LoanStatus(int loanId);

        // Method to calculate EMI by loanId
        decimal CalculateEMI(int loanId);

        // Overloaded method to calculate EMI with parameters
        decimal CalculateEMI(decimal principalAmount, decimal interestRate, int loanTerm);

        // New method to process loan repayment
        void LoanRepayment(int loanId, decimal amount);

        // New method to get all loans
        List<Loan> GetAllLoans();

        // New method to get loan by ID
        Loan GetLoanById(int loanId);
    }
}
