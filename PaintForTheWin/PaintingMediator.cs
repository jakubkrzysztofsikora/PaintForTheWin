using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaintForTheWin.CanvasComponents;
using PaintForTheWin.Ecosystem;

namespace PaintForTheWin
{
    public class PaintingMediator
    {
        public void ChangeActiveColor(string newColorInHex)
        {
            throw new NotImplementedException();
        }

        public object GetActiveColor()
        {
            throw new NotImplementedException();
        }

        public void ChangeToolTo(eTool toolType)
        {
            throw new NotImplementedException();
        }

        public object GetCurrentTool()
        {
            throw new NotImplementedException();
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
