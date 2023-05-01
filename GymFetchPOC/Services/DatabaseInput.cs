using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GymFetchPOC.DataContracts;
using GymFetchPOC.Helpers;

namespace GymFetchPOC.Services
{
    public class DatabaseInput
    {
        private const string conStr = "Server=tcp:begymfetcher.database.windows.net,1433;Initial Catalog=GymData;Persist Security Info=False;User ID=buffadmin;Password=Olayemi$22;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public DatabaseInput()
        {

        }

        public void WriteGymToDatabase (Gym gym)
        {

                string insertGym = "insert into GymData VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9});";
                string query = string.Format(insertGym, 
                                             gym._id.StripString(),
                                             gym.googlePlaceId.StripString(),
                                             gym.address.StripString(),
                                             gym.website.StripString(),
                                             gym.name.StripString(),
                                             gym.geoLocation.lat.ToString().StripString(),
                                             gym.geoLocation.lng.ToString().StripString(),
                                             gym.internationalPhoneNumber.StripString(),
                                             gym.formattedPhoneNumber.StripString(),
                                             gym.GoogleRating.ToString().StripString()
                                            );
            this.ExecuteSqlQuery(query);
            this.WriteAmenitiesToDatabse(gym);
            this.WritePhotosToDatabse(gym);
        }

        private void WriteAmenitiesToDatabse (Gym gym)
        {
            foreach (var amenity in gym.amenities)
            {
                string insertAmenitiy = "insert into GymAmenities VALUES('{0}',{1});";
                string query = string.Format(insertAmenitiy,
                                             gym._id.StripString(),
                                             amenity.AmenityId.ToString().StripString());

            this.ExecuteSqlQuery(query);
            }
        }

        private void WritePhotosToDatabse(Gym gym)
        {
            foreach (var photo in gym.GooglePhoto)
            {
                string insertAmenitiy = "insert into GooglePhotos VALUES('{0}','{1}','{2}');";
                string query = string.Format(insertAmenitiy,
                                             gym._id.StripString(),
                                             photo.PhotoReference.StripString(),
                                             photo.Photourl.StripString());

                this.ExecuteSqlQuery(query);
            }
        }

        private void ExecuteSqlQuery(string query)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    string g = ex.Message;
                    ///MessageBody.Show(ex.Message);
                }
            }
        }
    }
}
