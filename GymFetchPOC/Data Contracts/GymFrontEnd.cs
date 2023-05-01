using System;
using System.Collections.Generic;
using GymFetchPOC.DataContracts;
using Newtonsoft.Json;

namespace BuffEngineer.GymCrawler.DataContracts
{
    public class GymFrontEnd
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

        // Geographic
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }

        // Geographic
        public string Region { get; set; }
        public string Accessories { get; set; }
        public string Amenities { get; set; }
        public string WorkoutsSupported { get; set; }
    }
}
