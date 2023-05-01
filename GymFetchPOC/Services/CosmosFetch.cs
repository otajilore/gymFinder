using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using GymFetchPOC.DataContracts;
using System.Collections.Generic;
using GymFetchPOC.Helpers;
using static GymFetchPOC.DataContracts.GymSearchResult;

namespace GymFetchPOC.Services
{
    public class CosmosFetch
    {
        // ADD THIS PART TO YOUR CODE
        private const string EndpointUrl = "https://buffengineer.documents.azure.com:443/";
        private const string PrimaryKey = "Sn9deqnUdpIzYxVjJbeRw16F7xIrYihQpUrbXacQRmZvRrTV9axFyVvCtCyuih9JvZDb63wmJVoYJv6U72tsAQ==";
        private DocumentClient client;

        public CosmosFetch()
        {

        }

        public void ConnectDatabase()
        {
            // ADD THIS PART TO YOUR CODE
            try
            {
                CosmosFetch p = new CosmosFetch();
                p.GetStartedDemo().Wait();
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }
            finally
            {
                Console.WriteLine("End of demo, press any key to exit.");
                ///Console.ReadKey();
            }
        }

        public async Task<GymSearchResult> FetchDataAsync(UserRequest userRequest)
        {
            return await this.FetchGymsAsync(userRequest);
        }

        private async Task GetStartedDemo()
        {
            this.client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
        }

        private async Task<GymSearchResult> FetchGymsAsync(UserRequest userRequest)
        {
            string databaseName = "BuffEngineerGyms";
            string collectionName = "GymData";
            var getGeocode = new GetGeocode();

            GeoLocation geocode = await getGeocode.GetGeoCode(userRequest.address);

            if (geocode == null)
            {
                var r = new GymSearchResult();
                r.statusCode = (int)ResponseCode.InvalidInputAddress;
                return r;
            }

            int distanceInMiles;
            if (userRequest.distance >= 0)
            {
                distanceInMiles = (int)DataConversion.ConvertMilesToMeters(userRequest.distance);
            }
            else
            {
                distanceInMiles = 5000;
            }

            ////string sqlQuery = string.Format("SELECT * FROM GymData WHERE ST_DISTANCE(GymData.location, {{ 'type': 'Point', 'coordinates':[{0}, {1}]}}) < {2}", geocode.lng.ToString(), geocode.lat.ToString(), distanceInMiles.ToString());

            string sqlQuery = "SELECT * FROM c where c.state = 'NY'";
            this.GetStartedDemo().Wait();

            // Set some common query options
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true };

            // Now execute the same query via direct SQL
            IQueryable<Gym> gyms = null;
            try
            {
                gyms = this.client.CreateDocumentQuery<Gym>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),sqlQuery, queryOptions);
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }

            string xjh = JsonConvert.SerializeObject(gyms);

            var foundGyms = new List<Gym>();
            foreach (Gym gym in gyms)
            {
                foundGyms.Add(gym);
            }

            var response = new GymSearchResult();


            response.searchAddress = geocode.formattedAddress;
            response.gymCount = foundGyms.Count;
            response.gyms = foundGyms;
            response.searchRadius = userRequest.distance.ToString();
            return response;
        }

        private Gym PopulateWixProperties (Gym gym, GeoLocation userLocation)
        {
            string googleMapBaseUrl = "https://www.google.com/maps/search/?api=1&query={0},{1}&query_place_id={2}";
            if (gym.GooglePhoto.Count > 0)
            {
                gym.DisplayImage = gym?.GooglePhoto?.First()?.Photourl ?? null;
            }

            var rnd = new Random();
            var keyAmenities = gym.amenities;

            if (string.IsNullOrWhiteSpace(gym.KeyAmenities))
            {
                foreach (var amenity in keyAmenities ?? new List<Amenity>())
                {
                    gym.KeyAmenities = gym.KeyAmenities + amenity.Name;
                    gym.KeyAmenities = gym.KeyAmenities + "\n";
                }
            }
            gym.GooglePhoto.Clear();
            gym.amenities.Clear();

            return gym;
        }
    }
}
