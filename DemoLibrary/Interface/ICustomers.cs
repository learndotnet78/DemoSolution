using DemoLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Interface
{
    public interface ICustomers
    {
        List<Customer> GetAllCustomer();
        Customer GetCustomer(int id);
    }
}
