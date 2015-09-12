using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brush = System.Windows.Media.Brush;
using Size = System.Windows.Size;

namespace PaintForTheWin.ProgramCommands
{
    public class LoadImage : IProgramCommand
    {
        private readonly Uri _pathToImage;
        private readonly int _actionToChange;
        private Brush _previousBackground;
        private UIElementCollection _previousChildren;
        private Size _previousSize;
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
            _previousSize = new Size(element.Width, element.Height);

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
            _canvasNode.Width = _previousSize.Width;
            _canvasNode.Height = _previousSize.Height;
        }

        public int GetActionToChangeId()
        {
            return _actionToChange;
        }

        private BitmapImage LoadImageFromUri(Uri path)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            img.UriSource = path;
            img.EndInit();

            return img;
        }
    }
}
