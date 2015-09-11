using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
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
        private IProgramCommand _currentCommandInAction;

        #region Event Handlers

        public void OnCanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            IProgramCommand action = _commandFactory.CreateDrawCommand(_currentTool, (Canvas) sender, e);
            _currentCommandInAction = action;

            _canvasService.Apply(action);
        }

        public void OnCanvasMouseMove(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState.Equals(MouseButtonState.Pressed) && _currentCommandInAction != null)
            {
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

        public object GetCanvasSize()
        {
            throw new NotImplementedException();
        }

        public void LoadImage(string uri)
        {
            Uri pathToImage = new Uri(uri);
            IProgramCommand loadAction = _commandFactory.CreateLoadImageCommand(pathToImage);

            _canvasService.Apply(loadAction);
        }

        public void Save(string expectedSaveLocationString)
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfDoneChanges()
        {
            throw new NotImplementedException();
        }

        public void Reverse(eDirection direction)
        {
            throw new NotImplementedException();
        }

        public void Rotate(int degrees)
        {
            throw new NotImplementedException();
        }
    }
}
