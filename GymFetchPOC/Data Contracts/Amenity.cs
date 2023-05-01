using System;
using System.Collections.Generic;

namespace GymFetchPOC.DataContracts
{
    public class Amenity
    {
        public int AmenityId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public List<string> SearchTerms { get; set; }

        public Amenity()
        {
            this.SearchTerms = new List<string>();
        }

        public Amenity(string amenityType, string amenityName, string[] searchTerms)
        {
            this.SearchTerms = new List<string>();

            this.Category = amenityType;
            this.Name = amenityName;

            foreach (var searchTerm in searchTerms)
            {
                this.SearchTerms.Add(searchTerm);
            }
        }
    }
}
