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
        private TransformGroup _transformationSettings;

        public Reverse(int actionToChange, eDirection direction)
        {
            _direction = direction;
            _actionToChange = actionToChange;
        }

        public void Execute(Canvas element)
        {
            InitializeSettings(element);
            ScaleTransform scaleTransform = _transformationSettings.Children[2] as ScaleTransform;

            if (CanvasInHorizontalPosition(element))
            {
                scaleTransform.CenterX = element.RenderSize.Width / 2;
                scaleTransform.CenterY = element.RenderSize.Height / 2;
            }
            else
            {
                scaleTransform.CenterX = element.RenderSize.Height / 2;
                scaleTransform.CenterY = element.RenderSize.Width / 2;
            }

            switch (_direction)
            {
                case eDirection.Horizontal:
                    scaleTransform.ScaleY *= -1;
                    break;
                case eDirection.Vertical:
                    scaleTransform.ScaleX *= -1;
                    break;
                default:
                    scaleTransform.ScaleX = 0;
                    scaleTransform.ScaleY = 0;
                    break;
            }
            
            element.RenderTransform = _transformationSettings;
            _canvasNode = element;
        }

        public void Retract()
        {
            ScaleTransform scaleTransform = _transformationSettings.Children[2] as ScaleTransform;

            switch (_direction)
            {
                case eDirection.Horizontal:
                    scaleTransform.ScaleY *= -1;
                    break;
                case eDirection.Vertical:
                    scaleTransform.ScaleX *= -1;
                    break;
            }

            _canvasNode.RenderTransform = _transformationSettings;
        }

        public int GetActionToChangeId()
        {
            return _actionToChange;
        }

        private bool CanvasInHorizontalPosition(Canvas canvas)
        {
            RotateTransform rotateSettings = _transformationSettings.Children[0] as RotateTransform;
            return Math.Abs(rotateSettings.Angle % 180) < 0.01 || Math.Abs(-rotateSettings.Angle % 180) < 0.01 || Math.Abs(rotateSettings.Angle) < 0.01;
        }

        private void InitializeSettings(Canvas element)
        {
            if (!(element.RenderTransform is TransformGroup))
            {

                _transformationSettings = new TransformGroup();
                _transformationSettings.Children.Add(new RotateTransform());
                _transformationSettings.Children.Add(new TranslateTransform());
                _transformationSettings.Children.Add(new ScaleTransform());
            }
            else
            {
                _transformationSettings = element.RenderTransform as TransformGroup;
            }
        }
    }
}
