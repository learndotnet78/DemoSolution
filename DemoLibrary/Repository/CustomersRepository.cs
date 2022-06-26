using DemoLibrary.Interface;
using DemoLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Repository
{
    public class CustomersRepository : ICustomers
    {
        List<Customer> lsCustomers = new List<Customer>
        {
            new Customer{CustomerId=1, FirstName="Sam", LastName="Neil", Address="New York" },
            new Customer{CustomerId=2, FirstName="Jake", LastName="Simon", Address="Texas" },
            new Customer{CustomerId=3, FirstName="San", LastName="Juan", Address="New Mexico" },
            new Customer{CustomerId=4, FirstName="Pedro", LastName="Pascal", Address="California" },
            new Customer{CustomerId=5, FirstName="Ram", LastName="Narayan", Address="Washinton DC" },
            new Customer{CustomerId=6, FirstName="Eli", LastName="Shaw", Address="New Jersey" },
        };

        public List<Customer> GetAllCustomer()
        {
            return lsCustomers;
        }

        public Customer GetCustomer(int id)
        {
            return lsCustomers.FirstOrDefault(x => x.CustomerId == id);

        }

    }
}
