using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BuffEngineer.GymCrawler.DataContracts;
using GymFetchPOC.DataContracts;
using GymFetchPOC.DataContracts.Places;

namespace GymFetchPOC.Helpers
{
    public static class Mapper
    {
        public static GeoLocation ToGeoLocation (this GoogleGeoCode googleGeoCode)
        {
            if (googleGeoCode == null || googleGeoCode.results.Count <= 0)
            {
                return null;
            }

            GeoLocation geoLocation = new GeoLocation(googleGeoCode.results.First().geometry.location.lat, googleGeoCode.results.First().geometry.location.lng);
            geoLocation.formattedAddress = googleGeoCode.results.First().formatted_address;
            return geoLocation;
        }

        public static Gym ToGym(this GymFetchPOC.DataContracts.Places.Result result, GeoLocation searchGeoLocation)
        {
            Gym gym = new Gym();

            gym._id = result.id;
            gym.name = result.name;
            gym.address = result.vicinity;
            gym.googlePlaceId = result.place_id;


            GeoLocation gymGeoLocation = new GeoLocation(result.geometry.location.lat, result.geometry.location.lng);
            gym.geoLocation = gymGeoLocation;
            gym.location = new CosmosDBPoint(gymGeoLocation.lng, gymGeoLocation.lat);
            return gym;
        }

        public static string StripString(this string input)
        {
            if (input == null){
                return string.Empty;
            }
            string removesingleQuotes = input.Replace("'", "");
            return removesingleQuotes.Replace("\"", "");

        }

        public static GymFrontEnd ToGymFrontEnd(this Gym backend)
        {
            GymFrontEnd gymFront = new GymFrontEnd();

            gymFront._id = backend._id;
            gymFront.geoLocation = backend.geoLocation;
            gymFront.name = backend.name;
            gymFront.address = backend.address;
            gymFront.website = backend.website;
            gymFront.internationalPhoneNumber = backend.internationalPhoneNumber;
            gymFront.formattedPhoneNumber = backend.formattedPhoneNumber;
            gymFront.GoogleRating = backend.GoogleRating;
            gymFront.googlePlaceId = backend.googlePlaceId;
            gymFront.DisplayImage = backend.DisplayImage;
            gymFront.city = backend.city;
            gymFront.state = backend.state;
            gymFront.country = backend.country;

            gymFront.Region = backend.city + '_' + backend.state;

            string accesorries = null;
            string amenities = null;
            string workoutsSupported = null;

            foreach (var amenity in backend.amenities ?? new List<Amenity>())
            {
                if (amenity.Category.ToLower() == "accessories")
                {
                    accesorries = Append(amenity, accesorries);
                }
                else if (amenity.Category.ToLower() == "workouts supported")
                {
                    workoutsSupported = Append(amenity, workoutsSupported);
                }
                else
                {
                    amenities = Append(amenity, amenities);
                }
            }

            gymFront.Accessories = accesorries;
            gymFront.Amenities = amenities;
            gymFront.WorkoutsSupported = workoutsSupported;

            return gymFront;
        }

        private static string Append(Amenity Amenity, string value)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            if (string.IsNullOrWhiteSpace(value))
            {
                value = ti.ToTitleCase(Amenity.Name.ToLower());
            }
            else
            {
                value = value + ", " + ti.ToTitleCase(Amenity.Name.ToLower());
            }

            return value;
        }
    }
}
