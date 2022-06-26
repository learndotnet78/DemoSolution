using Microsoft.AspNetCore.Mvc;
using DemoCustomerApi.Interface;
using DemoCustomerApi.Model;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace DemoCustomerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        public IActionResult Get()
        {
            try
            {
                List<Customer> customer = new List<Customer>();
                customer = _customers.GetAllCustomer();

                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {

                Customer customer = new Customer();
                customer = _customers.GetCustomer(id);

                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost("AddCustomer")]
        public HttpResponseMessage Post([FromBody] Customer customer)
        {
            try
            {
                _customers.AddCustomer(customer);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("UpdateCustomer")]
        public HttpResponseMessage Put([FromBody] Customer customer)
        {
            try
            {
                _customers.UpdateCustomer(customer);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("DeleteCustomer")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _customers.DeleteCustomer(id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
