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
        private PaintingColor _colorToOverlay;
        private Brush _oldBrush;
        private UIElement _sender;
        private int _actionToChange;

        public Fill(int actionToChange, PaintingColor newColor, UIElement sender)
        {
            _newColor = newColor;
            _sender = sender as UIElement;
            _actionToChange = actionToChange;

            switch (GetTypeOfObject(_sender))
            {
                case ePaintableObject.Canvas:
                    Canvas canvas = (Canvas)_sender;
                    SolidColorBrush canvasBrush = canvas.Background as SolidColorBrush;
                    _colorToOverlay = PaintingColor.CreateFromNativeObject(canvasBrush.Color);
                    break;
                case ePaintableObject.Shape:
                    Shape shape = (Shape)_sender;
                    SolidColorBrush shapeBrush = shape.Fill as SolidColorBrush;
                    _colorToOverlay = PaintingColor.CreateFromNativeObject(shapeBrush.Color);
                    break;
            }
        }

        public void Execute(Canvas element)
        {
            switch (GetTypeOfObject(_sender))
            {
                case ePaintableObject.Canvas:
                    _oldBrush = element.Background;
                    element.Background = new SolidColorBrush(_newColor.GetNativeColorObject());
                    break;
                case ePaintableObject.Shape:
                    Shape shape = (Shape)_sender;
                    _oldBrush = shape.Fill;
                    shape.Fill = new SolidColorBrush(_newColor.GetNativeColorObject());
                    break;
                case ePaintableObject.Line:
                    Shape undefinedShape = (Shape) _sender;
                    _oldBrush = undefinedShape.Stroke;
                    undefinedShape.Stroke = new SolidColorBrush(_newColor.GetNativeColorObject());
                    break;
            }
        }

        public void Retract()
        {
            switch (GetTypeOfObject(_sender))
            {
                case ePaintableObject.Canvas:
                    Canvas canvas = (Canvas)_sender;
                    canvas.Background = _oldBrush;
                    break;
                case ePaintableObject.Shape:
                    Shape shape = (Shape)_sender;
                    shape.Fill = _oldBrush;
                    break;
                case ePaintableObject.Line:
                    Shape undefinedShape = (Shape)_sender;
                    undefinedShape.Stroke = _oldBrush;
                    break;
            }
        }

        private ePaintableObject GetTypeOfObject(UIElement element)
        {
            if (_sender is Canvas)
                return ePaintableObject.Canvas;
            else if (_sender is Line)
                return ePaintableObject.Line;
            else if (_sender is Shape)
                return ePaintableObject.Shape;
            else
                return ePaintableObject.Default;
        }

        public int GetActionToChangeId()
        {
            return _actionToChange;
        }
    }
}
