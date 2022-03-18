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
        private int blurValue;
        
        public Image(String correctName, String imageUrl, int startingTime)
        {
            this.imageUrl = imageUrl;
            this.correctName = correctName;
            // using 10 seconds as startingTime
            // should scale blur based on image size
            this.blurValue = startingTime;
        }

        public String getImageUrl()
        {
            return imageUrl;
        }

        public String getCorrectName()
        {
            return correctName;
        }

        public int getBlurValue()
        {
            return blurValue;
        }

        public void decreaseBlur(int timeLeft)
        {
            // should scale blur based on image size and timeLeft
            blurValue = timeLeft;
        }

    }
}
