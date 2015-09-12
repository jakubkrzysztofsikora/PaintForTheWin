using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Moq;
using NUnit.Framework;
using PaintForTheWin;
using PaintForTheWin.CanvasComponents;
using PaintForTheWin.CanvasComponents.ChangeManagement;
using PaintForTheWin.Ecosystem;
using PaintForTheWin.Ecosystem.ToolComponents;
using PaintForTheWin.ProgramCommands;

namespace PaintTests
{
    public class FunctionalTests
    {
        [TestCase("#ffffff")]
        public void ShouldChangeActiveColorForCurrentTool(string newColorInHex)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();

            //When
            paint.ChangeActiveColor(newColorInHex);

            //Then
            PaintingColor newColor = PaintingColor.CreateFromHex(newColorInHex);
            Assert.AreEqual(newColor, paint.GetActiveColor());
        }

        [TestCase(eTool.Pencil)]
        public void ShouldChangeCurrentTool(eTool toolType)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();

            //When
            paint.ChangeToolTo(toolType);

            //Then
            Tool expectedTool = new Tool(toolType);
            Assert.AreEqual(expectedTool, paint.GetCurrentTool());
        }

        [TestCase(eTool.Pencil)]
        [TestCase(eTool.Ellipse)]
        [TestCase(eTool.Rectangle)]
        [TestCase(eTool.Line)]
        [TestCase(eTool.Rubber)]
        [STAThread]
        public void ShouldDrawWithCurrentToolOnCanvas(eTool currentTool)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            CanvasBackService canvasService = new CanvasBackService();
            Canvas canvasNode = new Canvas();

            canvasNode.MouseDown += paint.OnCanvasMouseDown;
            canvasService.SetCanvas(canvasNode);
            paint.SetCanvasService(canvasService);
            paint.ChangeToolTo(currentTool);
            int numberOfElementsOnCanvas = canvasNode.Children.Count;

            //When
            ImitateMouseDownOn(canvasNode);

            //Then
            Assert.AreNotEqual(numberOfElementsOnCanvas, canvasNode.Children.Count);
        }

        [TestCase(400, 800)]
        [STAThread]
        public void ShouldResizeCanvas(int newWidth, int newHeight)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            CanvasBackService canvasService = new CanvasBackService();
            Canvas canvasNode = new Canvas();

            canvasService.SetCanvas(canvasNode);
            paint.SetCanvasService(canvasService);

            //When
            paint.ChangeCanvasSize(newWidth, newHeight);

            //Then
            Size expectedSize = new Size(newWidth, newHeight);
            Assert.AreEqual(expectedSize, canvasService.GetSize());
        }

        [TestCase("g:\\test.bmp")]
        [STAThread]
        public void ShouldLoadBmpImage(string path)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            CanvasBackService canvasService = new CanvasBackService();
            Canvas canvasNode = new Canvas();

            canvasService.SetCanvas(canvasNode);
            paint.SetCanvasService(canvasService);

            //When
            paint.LoadImage(path);

            //Then
            //Loaded image should be in the background of canvas
            Uri pathUri = new Uri(path);
            BitmapImage expectedImage = new BitmapImage(pathUri);
            ImageBrush expectedBrush = new ImageBrush(expectedImage);

            ImageBrush actualBrush = canvasNode.Background as ImageBrush;
            Assert.AreEqual(expectedBrush.ImageSource.ToString(), actualBrush.ImageSource.ToString());
        }

        [TestCase("g:\\test.bmp")]
        [STAThread]
        public void ShouldSaveImageToBmp(string expectedSaveLocationString)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            CanvasBackService canvasService = new CanvasBackService();
            Canvas canvasNode = new Canvas();

            canvasService.SetCanvas(canvasNode);
            paint.SetCanvasService(canvasService);
            paint.LoadImage("g:\\test.bmp");

            //When
            paint.Save(expectedSaveLocationString);

            //Then
            Uri saveLocation = new Uri(expectedSaveLocationString);
            BitmapImage expectedImage = new BitmapImage(saveLocation);
            Assert.IsNotNull(expectedImage);
        }

        [TestCase(1)]
        [TestCase(3)]
        [STAThread]
        public void ShouldUndoChanges(int numberOfChangesToUndo)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            CanvasBackService canvasService = new CanvasBackService();
            Canvas canvasNode = new Canvas();

            canvasService.SetCanvas(canvasNode);
            paint.SetCanvasService(canvasService);

            for (int i = 0; i < numberOfChangesToUndo; i++)
            {
                paint.Rotate(90);
            }

            //When
            for (int i = 0; i < numberOfChangesToUndo; i++)
            {
                paint.Undo();
            }

            //Then
            int expectedNumberOfChangesOnStack = 0;
            Assert.AreEqual(expectedNumberOfChangesOnStack, paint.GetNumberOfDoneChanges());
        }

        [Test]
        [STAThread]
        public void ShouldFloodFillConsistentColorSpace()
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            CanvasBackService canvasService = new CanvasBackService();
            Canvas canvasNode = new Canvas();

            canvasService.SetCanvas(canvasNode);
            paint.SetCanvasService(canvasService);
            canvasNode.MouseDown += paint.OnCanvasMouseDown;

            paint.ChangeToolTo(eTool.Line);
            paint.ChangeActiveColor("#ffffff");
            ImitateMouseDownOn(canvasNode);
            paint.ChangeToolTo(eTool.Fill);
            paint.ChangeActiveColor("#000000");
            Line drawedLine = canvasNode.Children[0] as Line;
            drawedLine.MouseDown += paint.OnCanvasChildClick;

            //When
            ImitateMouseDownOn(drawedLine);

            //Then
            Assert.AreEqual(paint.GetActiveColor().ToString(),drawedLine.Stroke.ToString());
        }

        [TestCase(eDirection.Horizontal)]
        [TestCase(eDirection.Vertical)]
        [STAThread]
        public void ShouldReverseCanvas(eDirection direction)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            CanvasBackService canvasService = new CanvasBackService();
            Canvas canvasNode = new Canvas();

            canvasService.SetCanvas(canvasNode);
            paint.SetCanvasService(canvasService);

            //When
            paint.Reverse(direction);

            //Then
            ScaleTransform expectedTransform = new ScaleTransform();
            if (direction.Equals(eDirection.Horizontal))
                expectedTransform.ScaleY = -1;
            else if (direction.Equals(eDirection.Vertical))
                expectedTransform.ScaleX = -1;

            Assert.AreEqual(expectedTransform.Value, canvasNode.RenderTransform.Value);
        }

        [TestCase(90)]
        [TestCase(-90)]
        [TestCase(180)]
        [STAThread]
        public void ShouldRotateCanvas(int degrees)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            CanvasBackService canvasService = new CanvasBackService();
            Canvas canvasNode = new Canvas();

            canvasService.SetCanvas(canvasNode);
            paint.SetCanvasService(canvasService);

            //When
            paint.Rotate(degrees);

            //Then

            Assert.AreEqual(canvasNode.Height, canvasNode.Width);
            Assert.AreEqual(canvasNode.Width, canvasNode.Height);
        }

        private void ImitateMouseDownOn(UIElement element)
        {
            element.RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = Button.MouseDownEvent } );
        }
    }
}
