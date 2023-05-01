using System;
namespace GymFetchPOC.DataContracts
{
    public class UserRequest
    {
        public string address { get; set; }
        public int distance { get; set; }
        public string formattedAddress { get; set; }
    }
}
