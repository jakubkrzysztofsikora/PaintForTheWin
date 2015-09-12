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
        private TransformGroup _currentSettings;
        private Transform _oldSettings;

        public Rotate(int actionToChange, int degrees)
        {
            _degrees = degrees;
            _actionToChange = actionToChange;
        }

        public void Execute(Canvas element)
        {
            _oldSettings = element.RenderTransform.Clone();
            InitializeSettings(element);

            RotateTransform rotate = _currentSettings.Children[0] as RotateTransform;
            TranslateTransform translate = _currentSettings.Children[1] as TranslateTransform;
            
            SetRotateOfCanvas(element, rotate);

            if (!IsCanvasBeingFliped(rotate))
                SetTranslateOfCanvas(element, translate);
            else
            {
                if (_degrees != 180)
                    DontTranslate(translate);
                else
                    TranslateFlippedVerticalCanvas(translate);
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

        private void TranslateFlippedVerticalCanvas(TranslateTransform translate)
        {
            RotateTransform rotateSettings = _currentSettings.Children[0] as RotateTransform;

            if (rotateSettings.Angle % 270 == 0)
                translate.X *= -1;
            else
            {
                translate.Y *= -1;
                translate.X *= -1;
            }
               
        }

        private void SetRotateOfCanvas(Canvas canvas, RotateTransform rotate)
        {
            rotate.Angle += _degrees;

            if (!CanvasInHorizontalPosition() && IsCanvasBeingFliped(rotate))
            {
                rotate.CenterX = canvas.RenderSize.Height / 2;
                rotate.CenterY = canvas.RenderSize.Width / 2;
            }
            else
            {
                rotate.CenterX = canvas.RenderSize.Width / 2;
                rotate.CenterY = canvas.RenderSize.Height / 2;
            }
        }

        private void SetTranslateOfCanvas(Canvas canvas, TranslateTransform translate)
        {
            double newTranslateX = canvas.RenderSize.Height / 2 - canvas.RenderSize.Width / 2;
            double newTranslateY = canvas.RenderSize.Width / 2 - canvas.RenderSize.Height / 2;

            translate.X += newTranslateX;
            translate.Y += newTranslateY;
        }

        private void DontTranslate(TranslateTransform translate)
        {
            translate.X = 0;
            translate.Y = 0;
        }

        private bool IsCanvasBeingFliped(RotateTransform rotate)
        {
            return Math.Abs(rotate.Angle % 180) < 0.01 || _degrees == 180;
        }

        private bool CanvasInHorizontalPosition()
        {
            RotateTransform rotateSettings = _currentSettings.Children[0] as RotateTransform;
            return Math.Abs(rotateSettings.Angle % 180) < 0.01 || Math.Abs(-rotateSettings.Angle % 180) < 0.01;
        }

        private void InitializeSettings(Canvas element)
        {
            if (!(element.RenderTransform is TransformGroup))
            {

                _currentSettings = new TransformGroup();
                _currentSettings.Children.Add(new RotateTransform());
                _currentSettings.Children.Add(new TranslateTransform());
                _currentSettings.Children.Add(new ScaleTransform());
            }
            else
            {
                _currentSettings = element.RenderTransform as TransformGroup;
            }
        }
    }
}
