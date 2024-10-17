// InvalidLoanException.cs (in exception)
using System;

namespace exception
{
    public class InvalidLoanException : Exception
    {
        // Property to hold additional information, such as the loan ID
        public int LoanId { get; }

        // Default constructor
        public InvalidLoanException() : base("An invalid loan operation occurred.") { }

        // Constructor with a custom message
        public InvalidLoanException(string message) : base(message) { }

        // Constructor with a custom message and loan ID
        public InvalidLoanException(string message, int loanId) : base(message)
        {
            LoanId = loanId; // Store the loan ID
        }

        // Constructor with a custom message and inner exception
        public InvalidLoanException(string message, Exception innerException)
            : base(message, innerException) { }

        // Constructor with a custom message, inner exception, and loan ID
        public InvalidLoanException(string message, Exception innerException, int loanId)
            : base(message, innerException)
        {
            LoanId = loanId; // Store the loan ID
        }
    }
}
