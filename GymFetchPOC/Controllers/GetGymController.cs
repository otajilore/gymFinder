using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BuffEngineer.GymCrawler.DataContracts;
using GymFetchPOC.DatabaseConnector;
using GymFetchPOC.DataContracts;
using GymFetchPOC.DataContracts.Places;
using GymFetchPOC.GoogleAPI;
using GymFetchPOC.Helpers;
using GymFetchPOC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Newtonsoft.Json;

namespace GymFetchPOC.Controllers
{
    [Route("api/[controller]")]
    public class GetGymController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var fetch = new DatabaseFetch();
            fetch.FetchGyms();

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{address}")]
        public async Task<GymSearchResult> Index(UserRequest userRequest)
        {
            var gymfetch = new CosmosFetch();
            gymfetch.ConnectDatabase();
            var result = await gymfetch.FetchDataAsync(userRequest);

            /*var gymData = new FetchGymData();
            var gyms = await gymData.GetGymsAsync(userRequest);
            return gyms.ToArray();*/

            var frontEnd = new List<GymFrontEnd>();

            foreach(var gym in result.gyms)
            {
                frontEnd.Add(gym.ToGymFrontEnd());
            }

            string json = JsonConvert.SerializeObject(frontEnd.ToArray());
            //write string to file
            System.IO.File.WriteAllText(@"/Users/macbook/Desktop/NewYork.txt", json);
            return result;
        }

    }
}
