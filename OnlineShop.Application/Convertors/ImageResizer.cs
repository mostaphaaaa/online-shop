using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Convertors
{
    public class ImageResizer
    {
        public void ImageResize(string inputImagePath, string outputImagePath, int? Width, int? Height)
        {
            var customWidth = Width ?? 120;
            var customHeight = Height ?? 210;

            using (var image = Image.Load(inputImagePath))
            {
                image.Mutate(x => x.Resize(customWidth, customHeight));
                image.Save(outputImagePath, new JpegEncoder()
                {
                    Quality = 100
                });
            }
        }
    }
}
