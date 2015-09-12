using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        public void ClearCanvas()
        {
            _canvasNode.Background = new SolidColorBrush(Color.FromRgb(255,255,255));
            _canvasNode.Children.Clear();
            _canvasNode.RenderTransform = new TransformGroup();
            TransformGroup newTransformGroup = _canvasNode.RenderTransform as TransformGroup;
            newTransformGroup.Children.Add(new RotateTransform());
            newTransformGroup.Children.Add(new TranslateTransform());
            newTransformGroup.Children.Add(new ScaleTransform());
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
            Size actualRenderSize = GetActualRenderedSize();

            RenderTargetBitmap result = new RenderTargetBitmap((int)actualRenderSize.Width, (int)actualRenderSize.Height, 96d, 96d, PixelFormats.Pbgra32);
            DrawingVisual visual = GetPreparedCanvasVisualToRender(actualRenderSize);
            result.Render(visual);

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
                lastChild.PreviewMouseDown += paintingMediator.OnCanvasChildClick;
            }
        }

        private DrawingVisual GetPreparedCanvasVisualToRender(Size renderSize)
        {
            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext ctx = visual.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(_canvasNode);
                ctx.DrawRectangle(vb, null, new Rect(new Point(), renderSize));
            }

            return visual;
        }

        private Size GetActualRenderedSize()
        {
            Size result;

            if (_canvasNode.RenderTransform is TransformGroup)
            {
                TransformGroup transformSettings = _canvasNode.RenderTransform as TransformGroup;
                TranslateTransform translate = transformSettings.Children[1] as TranslateTransform;

                result = new Size(translate.X*2 + _canvasNode.Width, translate.Y*2 + _canvasNode.Height);
            }
            else
            {
                result = _canvasNode.RenderSize;
            }

            return result;
        }
    }
}
