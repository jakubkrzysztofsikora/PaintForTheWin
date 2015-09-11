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
using PaintForTheWin.ProgramCommands.DrawingStrategies;

namespace PaintForTheWin.ProgramCommands
{
    class RubbingStrategy : IDrawingStrategy
    {
        public UIElement Draw(Canvas element, Tool tool, Point startingPoint, Point currentPoint)
        {
            Line pencilDrawing = new Line();
            pencilDrawing.Fill = new SolidColorBrush(Color.FromArgb(0,255,255,255));
            pencilDrawing.X1 = currentPoint.X;
            pencilDrawing.X2 = currentPoint.X;
            pencilDrawing.Y1 = currentPoint.Y;
            pencilDrawing.Y2 = currentPoint.Y;

            element.Children.Add(pencilDrawing);

            return pencilDrawing;
        }
    }
}
