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
        private int _degrees;

        public Rotate(int degrees)
        {
            _degrees = degrees;
        }

        public void Execute(Canvas element)
        {
            RotateTransform rotateTransform = new RotateTransform(_degrees);
            element.RenderTransform = rotateTransform;
        }

        public void Retract()
        {
            throw new NotImplementedException();
        }
    }
}
