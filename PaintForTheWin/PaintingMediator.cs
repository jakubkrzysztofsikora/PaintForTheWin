using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaintForTheWin.CanvasComponents;
using PaintForTheWin.Ecosystem;
using PaintForTheWin.Ecosystem.ToolComponents;

namespace PaintForTheWin
{
    public class PaintingMediator
    {
        private Tool _currentTool = new Tool();

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
            throw new NotImplementedException();
        }

        public void ChangeCanvasSize(int newWidth, int newHeight)
        {
            throw new NotImplementedException();
        }

        public object GetCanvasSize()
        {
            throw new NotImplementedException();
        }

        public void LoadImage(string testBmp)
        {
            throw new NotImplementedException();
        }

        public object GetCurrentCanvasBackground()
        {
            throw new NotImplementedException();
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
