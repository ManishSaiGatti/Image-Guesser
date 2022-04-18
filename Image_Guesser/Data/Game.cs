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
            String[] arr = currentImage.getVerticalStrips();
            /*String[] arr = new string[10];

            arr[0] = "Strips/VertStrip_0.png";
            arr[1] = "Strips/VertStrip_1.png";
            arr[2] = "Strips/VertStrip_2.png";
            arr[3] = "Strips/VertStrip_3.png";
            arr[4] = "Strips/VertStrip_4.png";
            arr[5] = "Strips/VertStrip_5.png";
            arr[6] = "Strips/VertStrip_6.png";
            arr[7] = "Strips/VertStrip_7.png";
            arr[8] = "Strips/VertStrip_8.png";
            arr[9] = "Strips/VertStrip_9.png";
            */

            return arr;
        }
    }
}
