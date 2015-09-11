using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using PaintForTheWin.Ecosystem;

namespace PaintForTheWin.ProgramCommands
{
    public class Reverse : IProgramCommand
    {
        private eDirection _direction;
        private readonly int _actionToChange;
        private Canvas _canvasNode;
        private ScaleTransform _doneTransformation;

        public Reverse(int actionToChange, eDirection direction)
        {
            _direction = direction;
            _actionToChange = actionToChange;
        }

        public void Execute(Canvas element)
        {
            ScaleTransform newTransform = new ScaleTransform();

            switch (_direction)
            {
                case eDirection.Horizontal:
                    newTransform.ScaleX = -1;
                    break;
                case eDirection.Vertical:
                    newTransform.ScaleY = -1;
                    break;
                default:
                    newTransform.ScaleX = 0;
                    newTransform.ScaleY = 0;
                    break;
            }

            element.RenderTransform = newTransform;
            _doneTransformation = newTransform;
            _canvasNode = element;
        }

        public void Retract()
        {
            _doneTransformation.ScaleX *= -1;
            _doneTransformation.ScaleY *= -1;

            _canvasNode.RenderTransform = _doneTransformation;
        }

        public int GetActionToChangeId()
        {
            return _actionToChange;
        }
    }
}
