using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GymFetchPOC.DataContracts;
using GymFetchPOC.DataContracts.Places;
using GymFetchPOC.Services;
using GymFetchPOC.Helpers;

namespace GymFetchPOC
{
    /// <summary>
    /// Class to handle Fetching of GeoCoordinates from Google
    /// </summary>
    public class GetGeocode
    {
        private RestClient restClient;

        public GetGeocode()
        {
            this.restClient = new RestClient();
        }

        /// <summary>
        /// Returns a Geo-coordinate address from a list of strings
        /// </summary>
        /// <returns>The geo code.</returns>
        /// <param name="address">Address.</param>
        public async Task<GeoLocation> GetGeoCode(string address)
        {
            if (address == null)
            {
                return default(GeoLocation);
            }
            address.Replace("  ", " ");
            address.Replace(" ", "+");

            GoogleGeoCode googleGeoCode =  await this.restClient.Get<GoogleGeoCode>(string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key=AIzaSyCZ6DmSpE3mCkDm6XLLMPUXjqTRS-5ayJI", address));
            return googleGeoCode.ToGeoLocation();
        }
    }
}
