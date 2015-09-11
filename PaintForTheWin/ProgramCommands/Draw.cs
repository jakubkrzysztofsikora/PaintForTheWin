using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PaintForTheWin.Ecosystem;
using PaintForTheWin.Ecosystem.ToolComponents;
using PaintForTheWin.ProgramCommands.DrawingStrategies;

namespace PaintForTheWin.ProgramCommands
{
    public class Draw : IProgramCommand
    {
        private Tool _tool;
        private Point _startingPoint;
        private Point _currentPoint;
        private readonly int _actionToChange;
        private UIElement _addedElement;
        private Canvas _canvasNode;

        private IDrawingStrategy _drawingStrategy;

        public Draw(int actionToChange, Tool tool, Point startingPosition, IDrawingStrategy strategy)
        {
            _tool = tool;
            _startingPoint = startingPosition;
            _currentPoint = startingPosition;
            _drawingStrategy = strategy;
            _actionToChange = actionToChange;
        }

        public void Execute(Canvas element)
        {
            _canvasNode = element;

            if (!_tool.GetToolType().Equals(eTool.Pencil))
                Retract();

            _addedElement = _drawingStrategy.Draw(element, _tool, _startingPoint, _currentPoint);
        }

        public void Retract()
        {
            if (_canvasNode.Children.Count > 0)
                _canvasNode.Children.Remove(_addedElement);
        }

        public int GetActionToChangeId()
        {
            return _actionToChange;
        }

        public void SetNewCurrentPoint(Point newPosition)
        {
            _currentPoint = newPosition;
        }
    }
}
