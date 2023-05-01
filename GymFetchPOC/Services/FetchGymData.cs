using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GymFetchPOC.DataContracts;
using GymFetchPOC.DataContracts.Places;
using GymFetchPOC.GoogleAPI;
using GymFetchPOC.Helpers;
using GymFetchPOC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GymFetchPOC.Services
{
    public class FetchGymData
    {
        public async Task<Gym[]> GetGymsAsync(UserRequest userRequest)
        {
            var getGeocode = new GetGeocode();
            var getGymlist = new GetGymList();
            var getSpecifcGym = new GetSpecifcGym();


            var geocode = await getGeocode.GetGeoCode(userRequest.address);
            var gyms = await getGymlist.GetGyms(geocode, (double)userRequest.distance);
            var populateGyms = await getSpecifcGym.AddAmenities(gyms.ToList());

            return populateGyms;
        }
    }
}
