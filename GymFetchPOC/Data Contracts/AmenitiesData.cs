using System;
using System.Collections.Generic;

namespace GymFetchPOC.DataContracts
{
    public class AmenitiesData
    {
        public List<Amenity> amenities { get; set; }
        
        public AmenitiesData()
        {
            this.amenities = new List<Amenity>();
        }

    }
}
