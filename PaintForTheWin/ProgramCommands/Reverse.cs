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

        public Reverse(eDirection direction)
        {
            _direction = direction;
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
        }

        public void Retract()
        {
            throw new NotImplementedException();
        }
    }
}
