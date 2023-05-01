using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GymFetchPOC.DataContracts;

namespace GymFetchPOC.GoogleAPI
{
    public class ServiceCall
    {

        private const string URL = "https://maps.googleapis.com/maps/api/geocode/json";
        //private string urlParameters = "?address=129+Abuloma+Road+Port+Harcourt&key=AIzaSyApzJQB-L0KDqW9QVXFPB-h87fz5ci4QwY";

        public async Task<string> GetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        return mycontent;
                    }
                }
            }
        }
    }
}
