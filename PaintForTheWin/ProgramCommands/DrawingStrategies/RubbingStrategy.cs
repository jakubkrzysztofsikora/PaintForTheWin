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
        private Point _previousPoint;
        private bool _firstUsage = true;

        public UIElement Draw(Canvas element, Tool tool, Point startingPoint, Point currentPoint)
        {
            FirstUsageAction(startingPoint);
            Line rubber = new Line();
            rubber.Fill = new SolidColorBrush(Color.FromArgb(1,255,255,255));
            rubber.Stroke = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
            rubber.StrokeThickness = 5;
            rubber.X1 = _previousPoint.X;
            rubber.X2 = currentPoint.X;
            rubber.Y1 = _previousPoint.Y;
            rubber.Y2 = currentPoint.Y;

            element.Children.Add(rubber);
            _previousPoint = currentPoint;

            return rubber;
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
