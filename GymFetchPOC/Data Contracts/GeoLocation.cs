using System;
using System.Collections.Generic;

namespace GymFetchPOC.DataContracts
{
    public class GeoLocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public string formattedAddress { get; set; }

        public GeoLocation(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }
    }

    public class CosmosDBPoint
    {
        public string type { get; set; }
        public List<double> coordinates;

        public CosmosDBPoint(double lat, double lng)
        {
            this.type = "Point";
            coordinates = new List<double>();
            coordinates.Add(lat);
            coordinates.Add(lng);
        }
    }
}
