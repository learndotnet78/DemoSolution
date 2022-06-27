using DemoSecureApi.Model;
using DemoSecureApi.Repository;
using Microsoft.AspNetCore.Mvc;


namespace DemoSecureApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private IConfiguration configuration;
        private UserAuthenticate auth;
        private CustomerRepository repo;
        public CustomerController(ILogger<CustomerController> logger,IConfiguration _configuration)
        {
            _logger = logger;
            this.configuration = _configuration;
            auth = new UserAuthenticate(this.configuration);
            repo = new CustomerRepository(this.configuration);
        }


        [HttpGet()]
        public IEnumerable<Customer> Get()
        {
            string token = auth.AuthenticateUser();
            return repo.GetCustomers(token);
        }

        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            string token = auth.AuthenticateUser();
            return repo.GetCustomer(token, id);
        }




    }
}
