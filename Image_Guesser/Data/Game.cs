using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Image_Guesser.Data.Components;
using System.IO;
namespace Image_Guesser.Data
{
    public class Game
    {
        private Image currentImage;
        private int startingTime;

        private String[] directories;
        public Game()
        {
            startingTime = 10;
            directories = Directory.GetDirectories(Directory.GetCurrentDirectory()+ "\\wwwroot\\object_images_A-C");
            currentImage = new Image(directories, startingTime);// ("cat", "file:///C:/Users/s-msubotic/OneDrive%20-%20Lake%20Washington%20School%20District/aSenior%20Year%20%3BD/Advanced%20Projects/object_images_A-C/abacus/abacus_01b.jpg");
        }

        public String getCorrectWord()
        {
            return currentImage.getCorrectName();
        }

        public Image getCurrentImage()
        {

            return currentImage;
        }


        public void makeNewImage()
        {
            currentImage = new Image(directories, startingTime);
        }
        public int getBlurValue()
        {
            return currentImage.getBlurValue();
        }

        public int getStartingTime()
        {
            return startingTime;
        }

        public int getImgHeight()
        {
            return currentImage.getImgHeight();
        }

        public int getStripWidth()
        {
            return currentImage.getStripWidth();
        }

        public String[] getVertStrips()
        {
            String[] arr = currentImage.getVerticalStrips("wwwroot/" + currentImage.getImageUrl());
            //String[] arr = currentImage.getVerticalStrips("https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Gull_portrait_ca_usa.jpg/300px-Gull_portrait_ca_usa.jpg");
            return arr;
        }

        public int getNumStrips()
        {
            return currentImage.numStrips;
        }
    }
}
