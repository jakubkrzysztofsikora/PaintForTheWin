﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PaintForTheWin.CanvasComponents.ChangeManagement;
using PaintForTheWin.ProgramCommands;

namespace PaintForTheWin.CanvasComponents
{
    public class CanvasBackService
    {
        private Canvas _canvasNode;
        private readonly ChangeStack _changeStack = new ChangeStack();

        public void SetCanvas(Canvas canvas)
        {
            _canvasNode = canvas;
        }

        public void Apply(IProgramCommand action)
        {
            action.Execute(_canvasNode);
            _canvasNode.UpdateLayout();
            _canvasNode.InvalidateVisual();

            _changeStack.Push(action);
        }

        public Size GetSize()
        {
            Size canvasSize = new Size(_canvasNode.Width, _canvasNode.Height);

            return canvasSize;
        }

        public RenderTargetBitmap GetCanvasPreparedToSave()
        {
            RenderTargetBitmap result = new RenderTargetBitmap((int)_canvasNode.Width, (int)_canvasNode.Height, 96d, 96d, System.Windows.Media.PixelFormats.Pbgra32);
            
            result.Render(_canvasNode);

            return result;
        }

        public void UndoLastChange()
        {
            Change lastChange = _changeStack.Pop();

            if (lastChange.GetId() != 0)
                lastChange.Retract();

            _canvasNode.UpdateLayout();
            _canvasNode.InvalidateVisual();
        }

        public int GetChangeStackCount()
        {
            return _changeStack.GetCount();
        }

        public void AddEventHandlerToLastChild(PaintingMediator paintingMediator)
        {
            if (_canvasNode.Children.Count > 0)
            {
                UIElement lastChild = _canvasNode.Children[_canvasNode.Children.Count - 1] as UIElement;
                lastChild.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(paintingMediator.OnCanvasChildClick));
            }
        }
    }
}
