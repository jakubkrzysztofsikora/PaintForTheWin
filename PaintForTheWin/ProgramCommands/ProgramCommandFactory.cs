using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PaintForTheWin.Ecosystem;
using PaintForTheWin.Ecosystem.ToolComponents;
using PaintForTheWin.ProgramCommands.DrawingStrategies;

namespace PaintForTheWin.ProgramCommands
{
    public class ProgramCommandFactory
    {
        private int _actionToChange = 0;

        public IProgramCommand CreateDrawCommand(Tool tool, Canvas canvasNode, MouseButtonEventArgs eventDetails)
        {
            ++_actionToChange;
            switch (tool.GetToolType())
            {
                case eTool.Pencil:
                    return new Draw(_actionToChange, tool, eventDetails.GetPosition(canvasNode), new PencilStrategy());
                case eTool.Line:
                    return new Draw(_actionToChange, tool, eventDetails.GetPosition(canvasNode), new LineStrategy());
                case eTool.Ellipse:
                    return new Draw(_actionToChange, tool, eventDetails.GetPosition(canvasNode), new EllipseStrategy());
                case eTool.Rectangle:
                    return new Draw(_actionToChange, tool, eventDetails.GetPosition(canvasNode), new RectangleStrategy());
                case eTool.Rubber:
                    return new Draw(_actionToChange, tool, eventDetails.GetPosition(canvasNode), new RubbingStrategy());
                default:
                    return new Draw(_actionToChange, tool, eventDetails.GetPosition(canvasNode), new PencilStrategy());
            }
        }

        public IProgramCommand CreateReverseCommand(eDirection direction)
        {
            ++_actionToChange;
            return new Reverse(_actionToChange, direction);
        }

        public IProgramCommand CreateRotateCommand(int degrees)
        {
            ++_actionToChange;
            return new Rotate(_actionToChange, degrees);
        }

        public IProgramCommand CreateLoadImageCommand(Uri pathToImage)
        {
            ++_actionToChange;
            return new LoadImage(_actionToChange, pathToImage);
        }

        public IProgramCommand CreateResizeCommand(double newWidth, double newHeight)
        {
            ++_actionToChange;
            Size newSize = new Size(newWidth, newHeight);

            return new Resize(_actionToChange, newSize);
        }

        public IProgramCommand CreateFillCommand(Tool tool, UIElement sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            ++_actionToChange;
            return new Fill(_actionToChange, tool.GetColor(), sender);
        }
    }
}
