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
        private ScaleTransform _transformationSettings;

        public Reverse(int actionToChange, eDirection direction)
        {
            _direction = direction;
            _actionToChange = actionToChange;
        }

        public void Execute(Canvas element)
        {
            if (!(element.RenderTransform is ScaleTransform))
            {
                _transformationSettings = new ScaleTransform();
            }
            else
            {
                _transformationSettings = element.RenderTransform as ScaleTransform;
            }

            _transformationSettings.CenterX = element.RenderSize.Width / 2;
            _transformationSettings.CenterY = element.RenderSize.Height / 2;

            switch (_direction)
            {
                case eDirection.Horizontal:
                    _transformationSettings.ScaleY *= -1;
                    break;
                case eDirection.Vertical:
                    _transformationSettings.ScaleX *= -1;
                    break;
                default:
                    _transformationSettings.ScaleX = 0;
                    _transformationSettings.ScaleY = 0;
                    break;
            }
            
            element.RenderTransform = _transformationSettings;
            _canvasNode = element;
        }

        public void Retract()
        {
            switch (_direction)
            {
                case eDirection.Horizontal:
                    _transformationSettings.ScaleY *= -1;
                    break;
                case eDirection.Vertical:
                    _transformationSettings.ScaleX *= -1;
                    break;
            }

            _canvasNode.RenderTransform = _transformationSettings;
        }

        public int GetActionToChangeId()
        {
            return _actionToChange;
        }
    }
}
