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
    public class EllipseStrategy : IDrawingStrategy
    {
        public UIElement Draw(Canvas element, Tool tool, Point startingPoint, Point currentPoint)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(tool.GetNativeColorObject());
            ellipse.Width = currentPoint.X - startingPoint.X;
            ellipse.Height = currentPoint.Y - startingPoint.Y;

            Canvas.SetLeft(ellipse, startingPoint.X);
            Canvas.SetTop(ellipse, currentPoint.Y);

            element.Children.Add(ellipse);

            return ellipse;
            //todo: in all strategies - cover all possibilites of drawing startPint> currentPoint, startPoint<currentPoint etc.
        }
    }
}
