using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PaintForTheWin.Ecosystem;

namespace PaintForTheWin.ProgramCommands
{
    class Fill : IProgramCommand
    {
        private PaintingColor _newColor;
        private Brush _oldBrush;
        private Shape _sender;
        private int _actionToChange;

        public Fill(int actionToChange, PaintingColor newColor, UIElement sender)
        {
            _newColor = newColor;
            _sender = sender as Shape;
            _actionToChange = actionToChange;
        }

        public void Execute(Canvas element)
        {
            _oldBrush = _sender.Fill;
            _sender.Fill = new SolidColorBrush(_newColor.GetNativeColorObject());
        }

        public void Retract()
        {
            _sender.Fill = _oldBrush;
        }

        public int GetActionToChangeId()
        {
            return _actionToChange;
        }
    }
}
