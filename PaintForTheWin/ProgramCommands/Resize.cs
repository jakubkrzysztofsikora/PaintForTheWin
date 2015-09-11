using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PaintForTheWin.ProgramCommands
{
    public class Resize : IProgramCommand
    {
        private Size _newSize;
        private Size _oldSize;
        private Canvas _canvasNode;
        private readonly int _actionToChange;

        public Resize(int actionToChange, Size newSize)
        {
            _newSize = newSize;
            _actionToChange = actionToChange;
        }

        public void Execute(Canvas element)
        {
            _oldSize = element.RenderSize;

            element.Width = _newSize.Width;
            element.Height = _newSize.Height;
            element.RenderSize = _newSize;

            _canvasNode = element;
        }

        public void Retract()
        {
            _canvasNode.Width = _oldSize.Width;
            _canvasNode.Height = _oldSize.Height;
            _canvasNode.RenderSize = _oldSize;
        }

        public int GetActionToChangeId()
        {
            return _actionToChange;
        }
    }
}
