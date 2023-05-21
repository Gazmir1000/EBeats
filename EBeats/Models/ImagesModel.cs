namespace EBeats.Models
{
    public class ImagesModel
    {
        public List<string> ImagePaths { get; set; }

        public ImagesModel()
        {
            // Initialize the image paths in the constructor
            ImagePaths = new List<string>
            {
                "./NewFolder/Screenshot (30).png",
                "./NewFolder/Screenshot (31).png",
                "./NewFolder/Screenshot (32).png",
                "./NewFolder/Screenshot (33).png",
            };
        }
    }
}

