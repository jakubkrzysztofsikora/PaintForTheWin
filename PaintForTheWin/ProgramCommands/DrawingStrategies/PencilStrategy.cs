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
        private Point _previousPoint;
        private bool _firstUsage = true;

        public UIElement Draw(Canvas canvas, Tool tool, Point startingPoint, Point currentPoint)
        {
            FirstUsageAction(startingPoint);
            Line pencilDrawing = new Line();
            pencilDrawing.Fill = new SolidColorBrush(tool.GetNativeColorObject());
            pencilDrawing.Stroke = new SolidColorBrush(tool.GetNativeColorObject());
            pencilDrawing.X1 = _previousPoint.X;
            pencilDrawing.X2 = currentPoint.X;
            pencilDrawing.Y1 = _previousPoint.Y;
            pencilDrawing.Y2 = currentPoint.Y;

            canvas.Children.Add(pencilDrawing);
            _previousPoint = currentPoint;

            return pencilDrawing;
        }

        private void FirstUsageAction(Point startingPoint)
        {
            if (_firstUsage)
            {
                _previousPoint = startingPoint;
                _firstUsage = false;
            }
        }
    }
}
