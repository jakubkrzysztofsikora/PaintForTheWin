using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using PaintForTheWin.Ecosystem.ToolComponents;

namespace PaintForTheWin.ProgramCommands.DrawingStrategies
{
    public interface IDrawingStrategy
    {
        void Draw(Canvas element, Tool tool, Point startingPoint, Point currentPoint);
    }
}
