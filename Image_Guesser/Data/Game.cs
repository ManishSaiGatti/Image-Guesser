using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Guesser.Data
{
    public class Game
    {
        private String correctWord;

        public Game()
        {
            correctWord = "cat";
        }

        public String getCorrectWord()
        {
            return correctWord;
        }
    }
}
