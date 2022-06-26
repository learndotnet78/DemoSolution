using DemoCustomerApi.Model;

namespace DemoCustomerApi.Interface
{
    public interface ICustomers
    {
        List<Customer> GetAllCustomer();
        Customer GetCustomer(int id);
        void AddCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void DeleteCustomer(int id);

    }
}
