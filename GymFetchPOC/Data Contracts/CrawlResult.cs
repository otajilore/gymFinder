using System;
using System.Collections.Generic;

namespace GymFetchPOC.DataContracts
{
    public class CrawlResult
    {

        public List<Gym> gymFound { get; set; }
        public List<Gym> gymsRemoved { get; set; }

        public CrawlResult()
        {
            gymFound = new List<Gym>();
            gymsRemoved = new List<Gym>();
        }
    }
}
