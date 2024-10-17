// Customer.cs (in entityLibrary)
namespace entityLibrary
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CreditScore { get; set; }

        // Default constructor
        public Customer() { }

        // Overloaded constructor
        public Customer(int customerID, string name, string emailAddress, string phoneNumber, string address, int creditScore)
        {
            CustomerID = customerID;
            Name = name;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            Address = address;
            CreditScore = creditScore;
        }

        // Method to print all information
        public void PrintCustomerInfo()
        {
            Console.WriteLine($"Customer ID: {CustomerID}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email Address: {EmailAddress}");
            Console.WriteLine($"Phone Number: {PhoneNumber}");
            Console.WriteLine($"Address: {Address}");
            Console.WriteLine($"Credit Score: {CreditScore}");
        }
    }
}

