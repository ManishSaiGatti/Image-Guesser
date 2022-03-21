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

        private int imgHeight = 0;
        private int stripWidth = 0;

        public Image(String correctName, String imageUrl)
        {
            this.imageUrl = imageUrl;
            this.correctName = correctName;
            getVerticalStrips();
        }

        public String getImageUrl()
        {
            return imageUrl;
        }

        public String getCorrectName()
        {
            return correctName;
        }

        public int getImgHeight()
        {
            return imgHeight;
        }

        public int getStripWidth()
        {
            return stripWidth;
        }

        public System.Drawing.Image[] getVerticalStrips()
        {
            System.Drawing.Image[] strips;

            using (var wc = new WebClient())
            {
                using (var stream = new MemoryStream(wc.DownloadData(imageUrl)))
                {
                    using (var img = System.Drawing.Image.FromStream(stream))
                    {
                        //do stuff with the image
                        int width = img.Width;
                        imgHeight = img.Height;

                        stripWidth = width / 10;

                        strips = new System.Drawing.Image[width / stripWidth];

                        //Graphics graphic = Graphics.FromImage(new Bitmap(img));
                        Bitmap bitmap = new Bitmap(img);
                        int index = 0;
                        for(int i = 0; i < width; i += stripWidth)
                        {
                            Rectangle boundaries = new Rectangle();
                            boundaries.X = i;
                            boundaries.Width = stripWidth;
                            boundaries.Height = img.Height;
                            Bitmap strip = bitmap.Clone(boundaries, bitmap.PixelFormat);
                            strips[index] = strip;
                            using (var graphic = Graphics.FromImage(strip))
                            {
                                //graphic.Clear(Color.Blue);
                                graphic.DrawImage(strip, new Point(index * stripWidth + 5, 0));
                                String savePath = "C:/Users/s-mgatti/Source/Repos/choice-project-image-guesser/Image_Guesser/wwwroot/Strips/VertStrip_" + index + ".png";
                                strip.Save(savePath);
                                // C:\Users\s-mgatti\Source\Repos\choice-project-image-guesser\Image_Guesser\Data\Strips\
                            }
                            index++;
                        }
                    }
                }
            }

            return strips;
        }
    }
}
