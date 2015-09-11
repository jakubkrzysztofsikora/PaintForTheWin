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
    public class RectangleStrategy : IDrawingStrategy
    {
        public UIElement Draw(Canvas element, Tool tool, Point startingPoint, Point currentPoint)
        {
            Rectangle rectangle = new Rectangle();
            DrawingShapeSetuper shapeSetuper = new DrawingShapeSetuper();
            rectangle.Fill = new SolidColorBrush(tool.GetNativeColorObject());
            rectangle.Width = shapeSetuper.GetWidthOfShape(startingPoint, currentPoint);
            rectangle.Height = shapeSetuper.GetHeightOfShape(startingPoint, currentPoint);

            shapeSetuper.SetUpShape(rectangle, startingPoint, currentPoint);

            element.Children.Add(rectangle);

            return rectangle;
        }
    }
}
