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
        
        public Image(String correctName, String imageUrl)
        {
            this.imageUrl = imageUrl;
            this.correctName = correctName;
        }

        public String getImageUrl()
        {
            return imageUrl;
        }
    }
}
