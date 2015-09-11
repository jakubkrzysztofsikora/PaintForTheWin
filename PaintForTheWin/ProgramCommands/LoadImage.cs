using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PaintForTheWin.ProgramCommands
{
    public class LoadImage : IProgramCommand
    {
        private readonly Uri _pathToImage;
        private readonly int _actionToChange;
        private Brush _previousBackground;
        private UIElementCollection _previousChildren;
        private Canvas _canvasNode;

        public LoadImage(int actionToChange, Uri pathToImage)
        {
            _pathToImage = pathToImage;
            _actionToChange = actionToChange;
        }

        public void Execute(Canvas element)
        {
            _previousChildren = element.Children;
            _previousBackground = element.Background;

            element.Children.Clear();
            ImageBrush imageBrush = new ImageBrush { ImageSource = LoadImageFromUri(_pathToImage) };
            element.Width = imageBrush.ImageSource.Width;
            element.Height = imageBrush.ImageSource.Height;
            Size imageSize = new Size(imageBrush.ImageSource.Width, imageBrush.ImageSource.Height);
            element.RenderSize = imageSize;
            element.Background = imageBrush;

            _canvasNode = element;
        }

        public void Retract()
        {
            _canvasNode.Children.Clear();

            foreach (UIElement child in _previousChildren)
            {
                _canvasNode.Children.Add(child);
            }

            _canvasNode.Background = _previousBackground;
        }

        public int GetActionToChangeId()
        {
            return _actionToChange;
        }

        private BitmapImage LoadImageFromUri(Uri path)
        {
            return new BitmapImage(path);
        }
    }
}
