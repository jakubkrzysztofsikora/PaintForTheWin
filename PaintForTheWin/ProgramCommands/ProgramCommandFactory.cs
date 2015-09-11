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
        public IProgramCommand CreateDrawCommand(Tool tool, Canvas canvasNode, MouseButtonEventArgs eventDetails)
        {
            switch (tool.GetToolType())
            {
                case eTool.Pencil:
                    return new Draw(tool, eventDetails.GetPosition(canvasNode), new PencilStrategy());
                case eTool.Line:
                    return new Draw(tool, eventDetails.GetPosition(canvasNode), new LineStrategy());
                case eTool.Ellipse:
                    return new Draw(tool, eventDetails.GetPosition(canvasNode), new EllipseStrategy());
                case eTool.Rectangle:
                    return new Draw(tool, eventDetails.GetPosition(canvasNode), new RectangleStrategy());
                default:
                    return new Draw(tool, eventDetails.GetPosition(canvasNode), new PencilStrategy());
            }
        }

        public IProgramCommand CreateReverseCommand(eDirection direction)
        {
            throw new NotImplementedException();
        }

        public IProgramCommand CreateRotateCommand(int degrees)
        {
            throw new NotImplementedException();
        }

        public IProgramCommand CreateLoadImageCommand(Uri pathToImage)
        {
            return new LoadImage(pathToImage);
        }

        public IProgramCommand CreateResizeCommand(double newWidth, double newHeight)
        {
            Size newSize = new Size(newWidth, newHeight);

            return new Resize(newSize);
        }
    }
}
