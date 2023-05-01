using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymFetchPOC.DataContracts;
using GymFetchPOC.DataContracts.Places;
using GymFetchPOC.Helpers;

namespace GymFetchPOC.Services
{
    public class GetGymList
    {
        private RestClient restClient;

        public GetGymList()
        {
            this.restClient = new RestClient();
        }

        /// <summary>
        /// Fetch Gyms from their respective locations
        /// </summary>
        /// <returns>The gyms.</returns>
        /// <param name="location">Location.</param>
        /// <param name="distance">Distance.</param>
        public async Task<IList<Gym>> GetGyms(GeoLocation location, double distance)
        {
            List<Gym> gyms = new List<Gym>();

            if (location  == null)
            {
                return gyms;
            }

            int distanceInMiles;
            if (distance >= 0)
            {
                distanceInMiles = (int)DataConversion.ConvertMilesToMeters(distance);
            }
            else
            {
                distanceInMiles = 5000;
            }

            Places places = await this.restClient.Get<Places>(string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius={2}&types=gym&name=&key=AIzaSyCZ6DmSpE3mCkDm6XLLMPUXjqTRS-5ayJI", location.lat.ToString(), location.lng.ToString(), distanceInMiles.ToString()));


            foreach (GymFetchPOC.DataContracts.Places.Result result in places.results)
            {
                if (IsStore(result.types.ToList()))
                {
                    gyms.Add(result.ToGym(location));
                }
            }

            return gyms;
        }

        private bool IsStore (List<string> strings)
        {
            bool answer = true;
            foreach (var word in strings)
            {
                if (word.ToLower().Contains("store"))
                {
                    answer = false;
                    break;
                }
            }

            return answer;
        }
    }
}
