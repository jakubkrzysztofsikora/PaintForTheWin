using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PaintForTheWin.Ecosystem.ToolComponents;
using PaintForTheWin.ProgramCommands.DrawingStrategies;

namespace PaintForTheWin.ProgramCommands
{
    public class Draw : IProgramCommand
    {
        private Tool _tool;
        private Point _startingPoint;
        private Point _currentPoint;

        private IDrawingStrategy _drawingStrategy;

        public Draw(Tool tool, Point startingPosition, IDrawingStrategy strategy)
        {
            _tool = tool;
            _startingPoint = startingPosition;
            _currentPoint = startingPosition;
            _drawingStrategy = strategy;
        }

        public void Execute(Canvas element)
        {
            _drawingStrategy.Draw(element, _tool, _startingPoint, _currentPoint);
        }

        public void Retract()
        {
            throw new NotImplementedException();
        }
    }
}
