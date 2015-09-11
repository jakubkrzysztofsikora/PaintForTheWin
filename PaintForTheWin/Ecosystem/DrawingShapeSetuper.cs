using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PaintForTheWin.Ecosystem
{
    public class DrawingShapeSetuper
    {
        public void SetUpShape(Shape shape, Point startingPoint, Point currentPoint)
        {
            switch (GetDirectionOfDrawing(startingPoint, currentPoint))
            {
                case eDirectionOfDrawing.DownLeft:
                    Canvas.SetLeft(shape, currentPoint.X);
                    Canvas.SetTop(shape, startingPoint.Y);
                    break;
                case eDirectionOfDrawing.UpLeft:
                    Canvas.SetLeft(shape, currentPoint.X);
                    Canvas.SetTop(shape, currentPoint.Y);
                    break;
                case eDirectionOfDrawing.UpRight:
                    Canvas.SetLeft(shape, startingPoint.X);
                    Canvas.SetTop(shape, currentPoint.Y);
                    break;
                case eDirectionOfDrawing.DownRight:
                    Canvas.SetLeft(shape, startingPoint.X);
                    Canvas.SetTop(shape, startingPoint.Y);
                    break;
                default:
                    break;
            }
        }

        public double GetWidthOfShape(Point startingPoint, Point currentPoint)
        {
            double result = currentPoint.X - startingPoint.X;

            if (result < 0)
                result *= -1;
            return result;
        }

        public double GetHeightOfShape(Point startingPoint, Point currentPoint)
        {
            double result = currentPoint.Y - startingPoint.Y;

            if (result < 0)
                result *= -1;
            return result;
        }

        private eDirectionOfDrawing GetDirectionOfDrawing(Point startingPoint, Point currentPoint)
        {
            double differenceInX = currentPoint.X - startingPoint.X;
            double differenceInY = currentPoint.Y - startingPoint.Y;

            if (differenceInX > 0 && differenceInY > 0)
                return eDirectionOfDrawing.DownRight;
            else if (differenceInX < 0 && differenceInY > 0)
                return eDirectionOfDrawing.DownLeft;
            else if (differenceInX < 0 && differenceInY < 0)
                return eDirectionOfDrawing.UpLeft;
            else if (differenceInX > 0 && differenceInY < 0)
                return eDirectionOfDrawing.UpRight;
            else
                return eDirectionOfDrawing.Default;
        }
    }
}
