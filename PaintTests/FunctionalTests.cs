using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public void ShouldDrawWithCurrentToolOnCanvas(eTool currentTool)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            CanvasBackService canvasService = CanvasBackService();
            Mock<Canvas> canvasMock = new Mock<Canvas>();

            canvasService.SetCanvas(canvasMock);
            paint.SetCanvasService(canvasService);
            paint.ChangeTool(currentTool);

            //When
            canvasMock.Object.RaiseEvent(new RoutedEventArgs(Canvas.MouseDownEvent));

            //Then
            canvasMock.Verify(canvas => canvas.Children.Add(new UIElement()));
        }
        

        [Test]
        public void ShouldRubElementsOnCanvas()
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            CanvasBackService canvasService = CanvasBackService();
            Mock<Canvas> canvasMock = new Mock<Canvas>();

            canvasService.SetCanvas(canvasMock);
            paint.SetCanvasService(canvasService);
            paint.ChangeTool(eTool.Rubber);

            //When
            canvasMock.Object.RaiseEvent(new RoutedEventArgs(Canvas.MouseDownEvent));

            //Then
            canvasMock.Verify(canvas => canvas.Children.Add(new UIElement()));
        }

        [TestCase(400, 800)]
        public void ShouldResizeCanvas(int newWidth, int newHeight)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();

            //When
            paint.ChangeCanvasSize(newWidth, newHeight);

            //Then
            CanvasSize expectedSize = new CanvasSize(newWidth, newHeight);
            Assert.AreEqual(expectedSize, paint.GetCanvasSize());
        }

        [Test]
        public void ShouldLoadBmpImage()
        {
            //Given
            PaintingMediator paint = new PaintingMediator();

            //When
            paint.LoadImage("test.bmp");

            //Then
            //Loaded image should be in the background of canvas
            BitmapImage expectedImage = new BitmapImage();
            Assert.AreEqual(expectedImage, paint.GetCurrentCanvasBackground());
        }

        [TestCase("testSave.bmp")]
        public void ShouldSaveImageToBMP(string expectedSaveLocationString)
        {
            //Given
            Mock<PaintingMediator> paintMock = new Mock<PaintingMediator>;
            CanvasBackService canvasManager = new CanvasBackService();

            //When
            paintMock.Save(expectedSaveLocationString);

            //Then
            Uri saveLocation = new Uri(expectedSaveLocationString);
            paintMock.Verify(paint => paint.SaveToFile(saveLocation, canvasManager.GetCanvasContents()));
        }

        [TestCase(1)]
        [TestCase(3)]
        public void ShouldUndoChanges(int numberOfChangesToUndo)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            ChangeStack changes = new ChangeStack();
            ProgramCommandFactory commandFactory = new ProgramCommandFactory();
            for (int i = 0; i < numberOfChangesToUndo; i++)
            {
                IProgramCommand action = commandFactory.CreateDrawCommand(paint.GetCurrentTool());
                changes.push(action);
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
        public void ShouldFloodFillConsistentColorSpace()
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            CanvasBackService canvasService = new CanvasBackService();
            Mock <Canvas> canvasMock = new Mock<Canvas>();
            Rectangle rectangle = new Rectangle();

            canvasMock.Children.Add(rectangle);
            canvasService.SetCanvas(canvasMock);
            paint.SetCanvasService(canvasServiceMock);
            paint.ChangeTool(eTool.FloodFill);

            //When
            //Triggered Mouse Event 
            
            //Then
            
        }

        [TestCase(eDirection.Horizontal)]
        [TestCase(eDirection.Vertical)]
        public void ShouldReverseCanvas(eDirection direction)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            Mock<CanvasBackService> canvasServiceMock = new Mock<CanvasBackService>();
            ProgramCommandFactory commandFactory = new ProgramCommandFactory();

            //When
            paint.Reverse(direction);

            //Then
            IProgramCommand reverseCommand = commandFactory.CreateReverseCommand(direction);
            canvasServiceMock.Verify(canvasService => canvasService.Apply(reverseCommand));
        }

        [TestCase(90)]
        [TestCase(-90)]
        [TestCase(180)]
        public void ShouldRotateCanvas(int degrees)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            Mock<CanvasBackService> canvasServiceMock = new Mock<CanvasBackService>();
            ProgramCommandFactory commandFactory = new ProgramCommandFactory();

            //When
            paint.Rotate(degrees);

            //Then
            IProgramCommand rotateCommand = commandFactory.CreateRotateCommand(degrees);
            canvasServiceMock.Verify(canvasService => canvasService.Apply(rotateCommand));
        }
    }
}
