using System;
using System.Collections.Generic;

namespace GymFetchPOC.DataContracts
{
    public class GymSearchResult
    {
        // Gym Information
        public enum ResponseCode : int { Success = 200, InvalidInputAddress = 201, NoGymsFound = 202, Error = 500};
        public int statusCode { get; set; }
        public int gymCount { get; set; }
        public string searchAddress { get; set; }
        public string searchRadius { get; set; }

        public List<Gym> gyms { get; set; }

    }
}
