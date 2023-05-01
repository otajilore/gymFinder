using System;
using GymFetchPOC.DataContracts;

namespace GymFetchPOC.Helpers
{
    public static class DataConversion
    {

        public static double ConvertMilesToMeters(double distance)
        {
            return Math.Round((double)(distance) * 1609.34, 2);
        }

        public static double ConvertMetersToMiles(double meters)
        {
            return (meters / 1609.344);
        }

        public static string GeoDistanceinMiles(GeoLocation geoLocation1, GeoLocation geoLocation2)
        {
            double lat1 = geoLocation1.lat;
            double lon1 = geoLocation1.lng;
            double lat2 = geoLocation2.lat;
            double lon2 = geoLocation2.lng;
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            dist = Math.Round(dist, 1);
            return dist.ToString();
        }
    }
}