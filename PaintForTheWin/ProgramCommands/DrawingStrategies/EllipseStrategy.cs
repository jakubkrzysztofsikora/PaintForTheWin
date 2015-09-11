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
    public class EllipseStrategy : IDrawingStrategy
    {
        public UIElement Draw(Canvas element, Tool tool, Point startingPoint, Point currentPoint)
        {
            DrawingShapeSetuper shapeSetuper = new DrawingShapeSetuper();
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(tool.GetNativeColorObject());
            ellipse.Width = shapeSetuper.GetWidthOfShape(startingPoint, currentPoint);
            ellipse.Height = shapeSetuper.GetHeightOfShape(startingPoint, currentPoint);
            shapeSetuper.SetUpShape(ellipse, startingPoint, currentPoint);

            element.Children.Add(ellipse);

            return ellipse;
        }
    }
}
