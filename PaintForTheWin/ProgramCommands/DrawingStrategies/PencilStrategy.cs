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
    public class PencilStrategy : IDrawingStrategy
    {
        public void Draw(Canvas canvas, Tool tool, Point startingPoint, Point currentPoint)
        {
            Line pencilDrawing = new Line();
            pencilDrawing.Fill = new SolidColorBrush(tool.GetNativeColorObject());
            pencilDrawing.X1 = currentPoint.X;
            pencilDrawing.X2 = currentPoint.X;
            pencilDrawing.Y1 = currentPoint.Y;
            pencilDrawing.Y2 = currentPoint.Y;
            
            canvas.Children.Add(pencilDrawing);
        }
    }
}
