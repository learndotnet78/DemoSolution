using Microsoft.AspNetCore.Mvc;
using DemoLibrary;
using DemoLibrary.Model;
using DemoLibrary.Interface;

namespace DemoSecureApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomers _customers;

        public CustomerController(ILogger<CustomerController> logger, ICustomers customers)
        {
            _logger = logger;
            _customers = customers;
        }

        [HttpGet()]
        public IEnumerable<Customer> Get()
        {
            return _customers.GetAllCustomer();
        }

        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _customers.GetCustomer(id);
        }
    }
}
