using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Guesser.Data.Components
{
    public class Image
    {
        private String correctName;
        private String imageUrl;
        private const String imgFolder = "object_images_A-C";
        public Image(String[] directories)
        {
            var rand = new Random();
            int randIndex = rand.Next(directories.Length);
            //this is to iterate through the path directory to find where the name of the image
            //starts
            int startIndex;
            for(int i = 0; i< directories[randIndex].Length; i++)
            {

            }
            imageUrl = directories[randIndex];

        }
        public Image(String correctName, String imageUrl)
        {
            this.imageUrl = imageUrl;
            this.correctName = correctName;
        }

        public String getImageUrl()
        {
            return imageUrl;
        }

        public String getCorrectName()
        {
            return correctName;
        }
    }
}
