using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace WebApp.Logic
{
    public class Picture
    {

        public static void SaveToFolder(Image img, Size newSize, string pathToSave)
        {
            // Get new resolution
            Size imgSize = NewImageSize(img.Size, newSize);
            using (Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                newImg.Save(pathToSave, img.RawFormat);
            }
        }

        public static Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                double tempval;
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                    tempval = newSize.Width / (imageSize.Width * 1.0);

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize; // image is already small size

            return finalSize;
        }
    }
}
