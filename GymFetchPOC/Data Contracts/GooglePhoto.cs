using System;
namespace GymFetchPOC.DataContracts
{
    public class GooglePhoto
    {
        public string PhotoReference { get; set; }
        public string Photourl { get; set; }

        public GooglePhoto(string PhotoReference, string PhotoUrl)
        {
            this.PhotoReference = PhotoReference;
            this.Photourl = PhotoUrl;
        }
    }
}
