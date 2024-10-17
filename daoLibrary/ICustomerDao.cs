using entityLibrary;
using System.Collections.Generic;

namespace daoLibrary
{
    public interface ICustomerDao
    {
        void AddCustomer(Customer customer);
        Customer GetCustomerById(int id);
        List<Customer> GetAllCustomers();
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}
