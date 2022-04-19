using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Image_Guesser.Data.Components
{
    public class Image
    {
        private String correctName;
        private String imageUrl;
        private int blurValue;
        private const String imgFolder = "object_images_A-C";

        private int imgHeight = 0;
        private int stripWidth = 0;

        public int numStrips;

        public Image(String[] directories, int startingTime)
        {
            var rand = new Random();
            int randIndex = rand.Next(directories.Length);
            //this is to iterate through the path directory to find where the name of the image
            //starts
            int startIndex = -1;

            for (int i = 0; i < directories[randIndex].Length - imgFolder.Length; i++)
            {
                if (directories[randIndex].Substring(i, imgFolder.Length).Equals(imgFolder))
                {
                    startIndex = i + imgFolder.Length + 1;
                }
            }
            correctName = directories[randIndex].Substring(startIndex);
            imageUrl = directories[randIndex].Substring(startIndex - (imgFolder.Length + 1)) + "\\" + correctName + "_0" + rand.Next(3, 10) + "s.jpg";
            correctName = correctName.Replace('_', ' ');
        }

        public Image(String correctName, String imageUrl, int startingTime)
        {
            this.imageUrl = imageUrl;
            this.correctName = correctName;
            // using 10 seconds as startingTime
            // should scale blur based on image size
            this.blurValue = startingTime;

            //getVerticalStrips();
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

        public int getImgHeight()
        {
            return imgHeight;
        }

        public int getStripWidth()
        {
            return stripWidth;
        }

        public String[] getVerticalStrips(String url)
        {
            String[] strips;

            /*using (var wc = new WebClient())
            {
                using (var stream = new MemoryStream(wc.DownloadData(url)))
                {
                    using (var img = System.Drawing.Image.FromStream(stream))
                    {
                        //do stuff with the image
                        int width = img.Width;
                        imgHeight = img.Height;

                        stripWidth = width / 50;

                        strips = new String[width / stripWidth];

                        //Graphics graphic = Graphics.FromImage(new Bitmap(img));
                        using (var bitmap = new Bitmap(img))
                        {
                            bitmap.Save("C:/Users/s-mgatti/Source/Repos/choice-project-image-guesser/Image_Guesser/wwwroot/Strips/Test.png");
                            int index = 0;
                            for (int i = 0; i < width; i += stripWidth)
                            {
                                Rectangle boundaries = new Rectangle();
                                boundaries.X = i;
                                boundaries.Width = stripWidth;
                                boundaries.Height = img.Height;
                                using (var strip = bitmap.Clone(boundaries, bitmap.PixelFormat))
                                {
                                    using (var graphic = Graphics.FromImage(strip))
                                    {
                                        //graphic.Clear(Color.Blue);
                                        graphic.DrawImage(strip, new Point(index * stripWidth, 0));
                                        String savePath = "C:/Users/s-mgatti/Source/Repos/choice-project-image-guesser/Image_Guesser/wwwroot/Strips/VertStrip_" + index + ".png";
                                        strip.Save(savePath);
                                        strips[index] = "Strips/VertStrip_" + index + ".png";
                                        // C:\Users\s-mgatti\Source\Repos\choice-project-image-guesser\Image_Guesser\Data\Strips\
                                    }
                                    index++;
                                }
                            }
                        }
                    }
                }
            }*/

            using (var img = System.Drawing.Image.FromFile(url))
            {
                //do stuff with the image
                int width = img.Width;
                imgHeight = img.Height;

                numStrips = getBiggestFactors(width);


                stripWidth = width / numStrips;

                strips = new String[numStrips];

                //Graphics graphic = Graphics.FromImage(new Bitmap(img));
                using (var bitmap = new Bitmap(img))
                {
                    bitmap.Save("C:/Users/s-mgatti/Source/Repos/choice-project-image-guesser/Image_Guesser/wwwroot/Strips/Test.png");
                    int index = 0;
                    for (int i = 0; i < numStrips; i ++)
                    {
                        Rectangle boundaries = new Rectangle();
                        boundaries.X = index;
                        boundaries.Width = stripWidth;
                        boundaries.Height = img.Height;
                        using (var strip = bitmap.Clone(boundaries, bitmap.PixelFormat))
                        {
                            using (var graphic = Graphics.FromImage(strip))
                            {
                                //graphic.Clear(Color.Blue);
                                graphic.DrawImage(strip, new Point(i * stripWidth, 0));
                                String savePath = "C:/Users/s-mgatti/Source/Repos/choice-project-image-guesser/Image_Guesser/wwwroot/Strips/VertStrip_" + index + ".png";
                                strip.Save(savePath);
                                strips[i] = "Strips/VertStrip_" + i + ".png";
                                // C:\Users\s-mgatti\Source\Repos\choice-project-image-guesser\Image_Guesser\Data\Strips\
                            }
                            index += stripWidth;
                        }
                    }
                }
            }

            return strips;
        }

        private int getBiggestFactors(int num)
        {
            int biggest = 0;
            for(int i = 1; i < Math.Sqrt(num) && i <= 50; i ++)
            {
                if(num % i == 0)
                {
                    if(i > biggest)
                    {
                        biggest = i;
                    }

                    if(num / i > biggest && num / i <= 50)
                    {
                        biggest = num / i;
                    }
                }
            }

            return biggest;
        }
    }
}
