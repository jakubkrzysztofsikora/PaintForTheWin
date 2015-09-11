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
        private readonly List<UIElement> _addedElements;
        private Canvas _canvasNode;

        private IDrawingStrategy _drawingStrategy;

        public Draw(int actionToChange, Tool tool, Point startingPosition, IDrawingStrategy strategy)
        {
            _tool = tool;
            _startingPoint = startingPosition;
            _currentPoint = startingPosition;
            _drawingStrategy = strategy;
            _actionToChange = actionToChange;
            _addedElements = new List<UIElement>();
        }

        public void Execute(Canvas element)
        {
            _canvasNode = element;

            if (!_tool.GetToolType().Equals(eTool.Pencil))
                Retract();

            UIElement addedElement = _drawingStrategy.Draw(element, _tool, _startingPoint, _currentPoint);

            _addedElements.Add(addedElement);
        }

        public void Retract()
        {
            if (_canvasNode.Children.Count > 0)
            {
                foreach (UIElement element in _addedElements)
                {
                    _canvasNode.Children.Remove(element);
                }
            }
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
