using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Image_Guesser.Data.Components;

namespace Image_Guesser.Data
{
    public class Game
    {
        private String correctWord;
        private Image currentImage;

        public Game()
        {
            currentImage = new Image("cat", "https://static01.nyt.com/images/2021/09/14/science/07CAT-STRIPES/07CAT-STRIPES-mediumSquareAt3X-v2.jpg");
        }

        public String getCorrectWord()
        {
            return correctWord;
        }

        public Image getCurrentImage()
        {
            return currentImage;
        }
    }
}
