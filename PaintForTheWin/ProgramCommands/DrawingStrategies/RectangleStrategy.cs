using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PaintForTheWin.Ecosystem.ToolComponents;

namespace PaintForTheWin.ProgramCommands.DrawingStrategies
{
    public class RectangleStrategy : IDrawingStrategy
    {
        public UIElement Draw(Canvas element, Tool tool, Point startingPoint, Point currentPoint)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = new SolidColorBrush(tool.GetNativeColorObject());
            rectangle.Width = currentPoint.X - startingPoint.X;
            rectangle.Height = currentPoint.Y - startingPoint.Y;

            Canvas.SetLeft(rectangle, startingPoint.X);
            Canvas.SetTop(rectangle, currentPoint.Y);

            element.Children.Add(rectangle);

            return rectangle;
        }
    }
}
