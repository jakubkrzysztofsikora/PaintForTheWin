using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace PaintForTheWin.ProgramCommands
{
    public class Rotate : IProgramCommand
    {
        private readonly int _degrees;
        private readonly int _actionToChange;
        private Canvas _canvasNode;

        public Rotate(int actionToChange, int degrees)
        {
            _degrees = degrees;
            _actionToChange = actionToChange;
        }

        public void Execute(Canvas element)
        {
            RotateTransform rotateTransform = new RotateTransform(_degrees);
            element.RenderTransform = rotateTransform;
            _canvasNode = element;
        }

        public void Retract()
        {
            RotateTransform rotateTransform = new RotateTransform(_degrees * -1);
            _canvasNode.RenderTransform = rotateTransform;
        }

        public int GetActionToChangeId()
        {
            return _actionToChange;
        }
    }
}
