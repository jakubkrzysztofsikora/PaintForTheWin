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
        private int _degrees;
        private bool _clockWise;
        private readonly int _actionToChange;
        private Canvas _canvasNode;
        private TransformGroup _currentSettings;
        private Transform _oldSettings;

        public Rotate(int actionToChange, int degrees)
        {
            _degrees = degrees;
            _actionToChange = actionToChange;

            if (_degrees < 0)
                _clockWise = false;
            else
                _clockWise = true;
        }

        public void Execute(Canvas element)
        {
            _oldSettings = element.RenderTransform.Clone();
            InitializeSettings(element);

            RotateTransform rotate = _currentSettings.Children[0] as RotateTransform;
            TranslateTransform translate = _currentSettings.Children[1] as TranslateTransform;
            rotate.Angle += _degrees;
            rotate.CenterX = element.RenderSize.Width / 2;
            rotate.CenterY = element.RenderSize.Height / 2;
            translate.X = element.RenderSize.Height / 2 - element.RenderSize.Width / 2;
            translate.Y = element.RenderSize.Width / 2 - element.RenderSize.Height / 2;

            if (!_clockWise)
            {
                translate.X *= -1;
                translate.Y *= -1;
            }
           
            element.RenderTransform = _currentSettings;
            _canvasNode = element;
        }

        public void Retract()
        {
            _canvasNode.RenderTransform = _oldSettings;
        }

        public int GetActionToChangeId()
        {
            return _actionToChange;
        }

        private void InitializeSettings(Canvas element)
        {
            if (!(element.RenderTransform is TransformGroup))
            {

                _currentSettings = new TransformGroup();
                _currentSettings.Children.Add(new RotateTransform());
                _currentSettings.Children.Add(new TranslateTransform());
            }
            else
            {
                _currentSettings = element.RenderTransform as TransformGroup;
            }
        }
    }
}
