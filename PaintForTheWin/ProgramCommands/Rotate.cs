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
            SetTranslateOfCanvas(element, rotate, translate);
           
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

        private void SetRotateOfCanvas(Canvas canvas, RotateTransform rotate)
        {
            rotate.Angle += _degrees;
            rotate.CenterX = canvas.RenderSize.Width / 2;
            rotate.CenterY = canvas.RenderSize.Height / 2;
        }

        private void SetTranslateOfCanvas(Canvas canvas, RotateTransform rotate, TranslateTransform translate)
        {
            double newTranslateX = canvas.RenderSize.Height / 2 - canvas.RenderSize.Width / 2;
            double newTranslateY = canvas.RenderSize.Width / 2 - canvas.RenderSize.Height / 2;

            if (!CanvasInHorizontalPosition(canvas))
            {
                newTranslateX *= -1;
                newTranslateY *= -1;
            }

            if (IsCanvasBeingFliped(rotate))
            {
                translate.X = 0;
                translate.Y = 0;
            }
            else
            {
                translate.X += newTranslateX;
                translate.Y += newTranslateY;
            }
        }

        private bool IsCanvasBeingFliped(RotateTransform rotate)
        {
            return Math.Abs(rotate.Angle - 180) < 0.01 || Math.Abs(rotate.Angle - (-180)) < 0.01;
        }

        private bool CanvasInHorizontalPosition(Canvas canvas)
        {
            return canvas.RenderTransform.Value.OffsetX >= 0;
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
