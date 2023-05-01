using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GymFetchPOC.Services
{
    public class RestClient
    {
        private HttpClient httpClient;


        public RestClient()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<T> Get<T>(string uri)
        {
            HttpResponseMessage responseMessage = await this.httpClient.GetAsync(uri);

            if (responseMessage.IsSuccessStatusCode)
            {
                string responseData = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseData);
            }

            return default(T);

         }
    }
}