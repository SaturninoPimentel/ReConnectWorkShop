using FacesApp.Helpers;
using System;

namespace FacesApp.Models
{
    public class Person
    {
        public string RowKey { get; set; }

        public string ImageFile { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public Uri ImageUri => new Uri(Constants.BlobStorageUrl + ImageFile);
    }
}