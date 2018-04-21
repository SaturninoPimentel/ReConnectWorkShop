using Microsoft.WindowsAzure.Storage.Table;

namespace FacesApp.Functions
{
    public class FaceRectangle : TableEntity
    {
        public string ImageFile { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }
    }
}