using System.Net.Http.Headers;
using DemoSecureApi.Model;
using Newtonsoft.Json;

namespace DemoSecureApi.Repository
{
    public class CustomerRepository
    {
        private readonly IConfiguration configuration;
        public CustomerRepository(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        public List<Customer> GetCustomers(string token)
        {
            List<Customer> customers = new List<Customer>();

            HttpClient client = GetHttpClient(token);

            HttpResponseMessage response = client.GetAsync(configuration.GetSection("APIUrls:Customer").Value).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;

            if (stringData != null)
            {
                customers = JsonConvert.DeserializeObject<List<Customer>>(stringData);
            }

            return customers;
        }


        public Customer GetCustomer(string token, int id)
        {
            Customer customer = new Customer();

            HttpClient client = GetHttpClient(token);
            HttpResponseMessage response = client.GetAsync(configuration.GetSection("APIUrls:Customer").Value + "/" + id.ToString()).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;

            if (stringData != null)
            {
                customer = JsonConvert.DeserializeObject<Customer>(stringData);
            }

            return customer;
        }


        public HttpClient GetHttpClient(string token)
        {
            string baseUrl = configuration.GetSection("APIUrls:Main").Value;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
    }
}
