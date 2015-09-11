using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PaintForTheWin.ProgramCommands;

namespace PaintForTheWin.CanvasComponents
{
    public class CanvasBackService
    {
        private Canvas _canvasNode;

        public void SetCanvas(Canvas canvas)
        {
            _canvasNode = canvas;
        }

        public void Apply(IProgramCommand action)
        {
            action.Execute(_canvasNode);
        }

        public Size GetSize()
        {
            Size canvasSize = new Size(_canvasNode.Width, _canvasNode.Height);

            return canvasSize;
        }
    }
}
