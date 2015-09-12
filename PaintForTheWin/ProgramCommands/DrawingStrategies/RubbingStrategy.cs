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
            Rectangle rubber = new Rectangle();
            DrawingShapeSetuper shapeSetuper = new DrawingShapeSetuper();
            rubber.Width = 10;
            rubber.Height = 10;
            shapeSetuper.SetUpShape(rubber, _previousPoint, currentPoint);
            rubber.Fill = new SolidColorBrush(Color.FromRgb(255,255,255));

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
