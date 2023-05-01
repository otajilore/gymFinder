using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GymFetchPOC.DataContracts;
using GymFetchPOC.DataContracts.GoogleSpecificPlace;
using GymFetchPOC.Helpers;
using Newtonsoft.Json;

namespace GymFetchPOC.Services
{
    /// <summary>
    /// Class to Fetch extensive details of a gym
    /// </summary>
    public class GetSpecifcGym
    {
        private const string genericPhotoUrl = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference={0}&key=AIzaSyCZ6DmSpE3mCkDm6XLLMPUXjqTRS-5ayJI";
        private RestClient restClient;

        public GetSpecifcGym()
        {
            this.restClient = new RestClient();
        }

        public async Task<Gym[]> AddAmenities(List<Gym> gyms)
        {
            List<Task<Gym>> fetchGymData = new List<Task<Gym>>();

            AmenitiesData dataModel = null;
            using (StreamReader r = new StreamReader("Data Model/AmenitiesData.json"))
            {
                string json = r.ReadToEnd();
                dataModel = JsonConvert.DeserializeObject<AmenitiesData>(json);
            }

            var amenities = dataModel.amenities;

            if (gyms.Count() >= 1)
            {
               foreach (var gym in gyms)
                {
                    fetchGymData.Add(GetPlaceDetails(gym, amenities));
                }
            }

            return await Task.WhenAll(fetchGymData);
        }

        private async Task<Gym> GetPlaceDetails(Gym gym, List<Amenity> amenities)
        {
            string id = gym.googlePlaceId;
            GoogleSpecificPlace gymDetails = await this.restClient.Get<GoogleSpecificPlace>(string.Format("https://maps.googleapis.com/maps/api/place/details/json?placeid={0}&key=AIzaSyCZ6DmSpE3mCkDm6XLLMPUXjqTRS-5ayJI", id));

            string gymWebsite = gymDetails.result.website;
            gym.website = gymWebsite;
            gym.internationalPhoneNumber = gymDetails.result.international_phone_number;
            gym.formattedPhoneNumber = gymDetails.result.formatted_phone_number;
            gym.GoogleRating = gymDetails.result.rating;

            if (gymDetails.result != null && gymDetails.result.photos != null)
            {

                foreach (var photo in gymDetails.result.photos)
                {
                    gym.GooglePhoto.Add(new GooglePhoto(photo.photo_reference, string.Format(genericPhotoUrl, photo.photo_reference)));
                }
            }

            var crawler = new Crawler();
            string x = crawler.crawlWebPage(gymWebsite).ToLower();

            foreach (var amenity in amenities)
            {
                foreach (var searchword in amenity.SearchTerms)
                {
                    string tagSearch = searchword.Replace(" ", "&nbsp;");
                    if (x.Contains(searchword.ToLower()) || x.Contains(tagSearch.ToLower()))
                    {
                        gym.amenities.Add(amenity);
                        break;
                    }
                }
            }

            return gym;
        }
    }
}
