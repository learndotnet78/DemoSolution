using System.Net.Http.Headers;
using DemoSecureApi.Model;
using Newtonsoft.Json;

namespace DemoSecureApi.Model
{
    public class UserAuthenticate
    {
        private readonly IConfiguration configuration;
        public UserAuthenticate(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        public string AuthenticateUser()
        {
            string stringJWT = string.Empty;

            string baseUrl = configuration.GetSection("APIUrls:Main").Value;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            User userModel = new User();
            userModel.UserName = configuration.GetSection("APICreadentials:UserName").Value;
            userModel.Password = configuration.GetSection("APICreadentials:Password").Value;

            string userData = JsonConvert.SerializeObject(userModel);

            var contentData = new StringContent(userData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("Auth/Login", contentData).Result;
            stringJWT = response.Content.ReadAsStringAsync().Result;
            if (!String.IsNullOrEmpty(stringJWT))
            {
                stringJWT = JsonConvert.DeserializeObject<string>(stringJWT);
            }

            return stringJWT;
        }

    }
}
