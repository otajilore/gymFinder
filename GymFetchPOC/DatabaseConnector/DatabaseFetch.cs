using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GymFetchPOC.DataContracts;

namespace GymFetchPOC.DatabaseConnector
{
    public class DatabaseFetch
    {
        private const string conStr = "Server=tcp:begymfetcher.database.windows.net,1433;Initial Catalog=GymData;Persist Security Info=False;User ID=buffadmin;Password=Olayemi$22;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public DatabaseFetch()
        {
            
        }

        public void FetchGyms ()
        {
            var gymResults = new List<Gym>();

            string FetchGym = "SELECT  GymData.name, GymAmenities.gymID, gymdata.internationalphonenumber, GymData.googlePlaceId, GymData.address, GymData.website, GymAmenities.amenityID, Amenities.name, amenities.description, gymdata.googlerating, googlephotos.photourl, (3959 *   acos(cos(radians(59.8659449685365)) * cos(radians(gymdata.lat)) *cos(radians(gymdata.lng) -radians(-4.15072511658071)) +sin(radians(59.8659449685365)) *sin(radians(gymdata.lat)))) AS distance FROM GymAmenities JOIN Amenities ON Amenities.AmenityID = GymAmenities.amenityID Join GymData ON GymData.gymID = GymAmenities.gymid Join googlephotos ON googlephotos.gymid = GymAmenities.gymid Where(3959 *acos(cos(radians(59.8659449685365)) *cos(radians(gymdata.lat)) *cos(radians(gymdata.lng) -radians(-4.25072511658072)) +sin(radians(59.8659449685365)) *sin(radians(gymdata.lat)))) < 4300 ORDER BY distance desc";
            using (SqlConnection connection = new SqlConnection(conStr))
            using (SqlCommand command = new SqlCommand(FetchGym, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (GymExists(gymResults, reader["gymId"].ToString()))
                        {
                            var gym = new Gym();
                            gym._id = reader["gymId"].ToString();
                            gymResults.Add(gym);
                        }
                    }
                }
            }
        }

        private bool GymExists (List<Gym> gyms, string id)
        {
            bool answer = true;

            foreach (var gym in gyms)
            {
                if (gym._id == id)
                {
                    answer = false;
                }
            }

            return answer;
        }
    }
}
