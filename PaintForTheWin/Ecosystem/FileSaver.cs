using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PaintForTheWin.Ecosystem
{
    public class FileSaver
    {
        public void Save(eFileExtension extenstion, Uri location, RenderTargetBitmap preparedCanvas)
        {
            BitmapEncoder imgEncoder = GetEncoder(extenstion);
            BitmapFrame outputFrame = BitmapFrame.Create(preparedCanvas);

            imgEncoder.Frames.Add(outputFrame);

            using (var file = File.OpenWrite(location.AbsolutePath))
            {
                imgEncoder.Save(file);
            }
        }

        private BitmapEncoder GetEncoder(eFileExtension extension)
        {
            switch (extension)
            {
                case eFileExtension.Bmp:
                    return new BmpBitmapEncoder();
                default:
                    return new BmpBitmapEncoder();
            }
        }
    }
}
