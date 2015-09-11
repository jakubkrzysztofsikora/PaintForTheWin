using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PaintForTheWin.ProgramCommands
{
    public class Rotate : IProgramCommand
    {
        private readonly int _degrees;
        private readonly int _actionToChange;
        private Canvas _canvasNode;
        private RotateTransform _currentSettings;

        public Rotate(int actionToChange, int degrees)
        {
            _degrees = degrees;
            _actionToChange = actionToChange;
        }

        public void Execute(Canvas element)
        {
            if (!(element.RenderTransform is RotateTransform))
            {
                _currentSettings = new RotateTransform(0);
            }
            else
            {
                _currentSettings = element.RenderTransform as RotateTransform;
            }

            _currentSettings.Angle += _degrees;
            _currentSettings.CenterX = element.RenderSize.Width / 2;
            _currentSettings.CenterY = element.RenderSize.Height / 2;
            _currentSettings.Value.Translate(0, -element.RenderSize.Height);
            
            element.RenderTransform = _currentSettings;
            _canvasNode = element;
        }

        public void Retract()
        {
            _currentSettings.Angle -= _degrees;

            _currentSettings.CenterX = _canvasNode.RenderSize.Width / 2;
            _currentSettings.CenterY = _canvasNode.RenderSize.Height / 2;
            _currentSettings.Value.Translate(0, -_canvasNode.RenderSize.Height);

            _canvasNode.RenderTransform = _currentSettings;
        }

        public int GetActionToChangeId()
        {
            return _actionToChange;
        }
    }
}
