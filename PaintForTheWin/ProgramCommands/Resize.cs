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

        public Resize(Size newSize)
        {
            _newSize = newSize;
        }

        public void Execute(Canvas element)
        {
            element.Width = _newSize.Width;
            element.Height = _newSize.Height;
        }

        public void Retract()
        {
            throw new NotImplementedException();
        }
    }
}
