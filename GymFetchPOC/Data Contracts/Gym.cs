using System;
using System.Collections.Generic;
using GymFetchPOC.DataContracts.GoogleSpecificPlace;
using Newtonsoft.Json;

namespace GymFetchPOC.DataContracts
{
    /*public class Gym
    {
        // MetaData
        [JsonProperty(PropertyName = "_id")]
        public string _id { get; set; }
        public GeoLocation geoLocation { get; set; }

        // Gym Information
        public string name { get; set; }
        public string address { get; set; }
        public string website { get; set; }

        public string GoogleMapUrl { get; set; }
        public string DisplayImage { get; set; }
        public string KeyAmenities { get; set; }
        public string distanceFromUser { get; set; }


        public string internationalPhoneNumber { get; set; }
        public string formattedPhoneNumber { get; set; }



        // Google SpecificInformation
        public double GoogleRating { get; set; }
        public string googlePlaceId { get; set; }
        public List<GooglePhoto> GooglePhoto { get; set; }
        public CosmosDBPoint location { get; set; }
        public List<Amenity> amenities { get; set; }

        public Gym()
        {
            amenities = new List<Amenity>();
            GooglePhoto = new List<GooglePhoto>();
        }
    }*/

    public class Gym
    {
        // BuffEngineer Identifier
        public Guid buffEngineerID { get; set; }

        // MetaData
        [JsonProperty(PropertyName = "_id")]
        public string _id { get; set; }
        public GeoLocation geoLocation { get; set; }

        // BuffEngineer Unique Identifier


        // Gym Information
        public string name { get; set; }
        public string address { get; set; }
        public string website { get; set; }

        public string internationalPhoneNumber { get; set; }
        public string formattedPhoneNumber { get; set; }


        // Google SpecificInformation
        public double GoogleRating { get; set; }
        public string googlePlaceId { get; set; }
        public string DisplayImage { get; set; }
        public string KeyAmenities { get; set; }

        // Geographic
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }


        public List<GooglePhoto> GooglePhoto { get; set; }
        public CosmosDBPoint location { get; set; }
        public List<Amenity> amenities { get; set; }
        public List<Review> googleReviews { get; set; }
        public int amenitiesCount { get; set; }

        public Gym()
        {
            amenities = new List<Amenity>();
            GooglePhoto = new List<GooglePhoto>();
            googleReviews = new List<Review>();
            buffEngineerID = Guid.NewGuid();

        }
    }
}
