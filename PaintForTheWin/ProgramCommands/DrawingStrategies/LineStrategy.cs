using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PaintForTheWin.Ecosystem;
using PaintForTheWin.Ecosystem.ToolComponents;

namespace PaintForTheWin.ProgramCommands.DrawingStrategies
{
    public class LineStrategy : IDrawingStrategy
    {
        public UIElement Draw(Canvas element, Tool tool, Point startingPoint, Point currentPoint)
        {
            Line line = new Line();
            line.Fill = new SolidColorBrush(tool.GetNativeColorObject());
            line.Stroke = new SolidColorBrush(tool.GetNativeColorObject());
            line.X1 = startingPoint.X;
            line.X2 = currentPoint.X;
            line.Y1 = startingPoint.Y;
            line.Y2 = currentPoint.Y;

            element.Children.Add(line);

            return line;
        }
    }
}
