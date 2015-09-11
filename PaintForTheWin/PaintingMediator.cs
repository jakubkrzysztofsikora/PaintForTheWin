using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using PaintForTheWin.CanvasComponents;
using PaintForTheWin.Ecosystem;
using PaintForTheWin.Ecosystem.ToolComponents;
using PaintForTheWin.ProgramCommands;

namespace PaintForTheWin
{
    public class PaintingMediator
    {
        private readonly Tool _currentTool = new Tool();
        private CanvasBackService _canvasService;
        private readonly ProgramCommandFactory _commandFactory = new ProgramCommandFactory();
        private Draw _currentCommandInAction;
        private FileSaver _saver = new FileSaver();

        #region Event Handlers

        public void OnCanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            Draw action = _commandFactory.CreateDrawCommand(_currentTool, (Canvas) sender, e) as Draw;
            _currentCommandInAction = action;

            _canvasService.Apply(action);
        }

        public void OnCanvasMouseMove(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState.Equals(MouseButtonState.Pressed) && _currentCommandInAction != null)
            {
                _currentCommandInAction.SetNewCurrentPoint(e.GetPosition( (UIElement) sender));
                _canvasService.Apply(_currentCommandInAction);
            }
        }

        public void OnCanvasMouseUp(object sender, MouseButtonEventArgs e)
        {
            _currentCommandInAction = null;
        }

        #endregion

        public void ChangeActiveColor(string newColorInHex)
        {
            _currentTool.ChangeColor(newColorInHex);
        }

        public object GetActiveColor()
        {
            return _currentTool.GetColor();
        }

        public void ChangeToolTo(eTool toolType)
        {
            _currentTool.ChangeType(toolType);
        }

        public object GetCurrentTool()
        {
            return _currentTool;
        }

        public void SetCanvasService(CanvasBackService canvasService)
        {
            _canvasService = canvasService;
        }

        public void ChangeCanvasSize(double newWidth, double newHeight)
        {
            IProgramCommand resizeAction = _commandFactory.CreateResizeCommand(newWidth, newHeight);

            _canvasService.Apply(resizeAction);
        }

        public void LoadImage(string uri)
        {
            Uri pathToImage = new Uri(uri);
            IProgramCommand loadAction = _commandFactory.CreateLoadImageCommand(pathToImage);

            _canvasService.Apply(loadAction);
        }

        public void Save(string saveLocationString)
        {
            Uri location = new Uri(saveLocationString);
            RenderTargetBitmap preparedCanvas = _canvasService.GetCanvasPreparedToSave();
            _saver.Save(eFileExtension.Bmp, location, preparedCanvas);
        }

        public void Undo()
        {
            _canvasService.UndoLastChange();
        }

        public int GetNumberOfDoneChanges()
        {
            return _canvasService.GetChangeStackCount();
        }

        public void Reverse(eDirection direction)
        {
            IProgramCommand reverseAction = _commandFactory.CreateReverseCommand(direction);
            _canvasService.Apply(reverseAction);
        }

        public void Rotate(int degrees)
        {
            IProgramCommand rotateAction = _commandFactory.CreateRotateCommand(degrees);
            _canvasService.Apply(rotateAction);
        }
    }
}
