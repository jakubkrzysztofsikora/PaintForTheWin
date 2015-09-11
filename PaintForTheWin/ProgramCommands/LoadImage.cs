using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PaintForTheWin.ProgramCommands
{
    public class LoadImage : IProgramCommand
    {
        private Uri _pathToImage;

        public LoadImage(Uri pathToImage)
        {
            _pathToImage = pathToImage;
        }

        public void Execute(Canvas element)
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = LoadImageFromUri(_pathToImage);
            element.Background = imageBrush;
        }

        public void Retract()
        {
            throw new NotImplementedException();
        }

        private BitmapImage LoadImageFromUri(Uri path)
        {
            return new BitmapImage(path);
        }
    }
}
