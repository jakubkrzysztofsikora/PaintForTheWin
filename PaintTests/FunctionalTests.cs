using System;
using NUnit.Framework;

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
            Color newColor = Color.CreateFromHex(newColorInHex);
            Assert.AreEqual(newColor, paint.GetActiveColor());
        }

        [TestCase(eTool.Pencil)]
        public void ShouldChangeCurrentTool(eTool tool)
        {
            //Given
            PaintingMediator paint = new PaintingMediator();

            //When
            paint.ChangeToolTo(tool);

            //Then
            Tool expectedTool = new Tool(tool);
            Assert.AreEqual(expectedTool, paint.GetCurrentTool());
        }

        [Test]
        public void ShouldDrawWithPencilOnCanvas()
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            paint.ChangeTool(eTool.Pencil);

            //When
            //Triggered MouseDown event

            //Then
            //Pencil drawing element has been added to canvas
        }

        [Test]
        public void ShouldDrawEllipseOnCanvas()
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            paint.ChangeTool(eTool.Ellipse);

            //When
            //Triggered MouseUp event

            //Then
            //Ellipse drawing element has been added to canvas
        }

        [Test]
        public void ShouldDrawRectangleOnCanvas()
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            paint.ChangeTool(eTool.Rectangle);

            //When
            //Triggered MouseUp event

            //Then
            //Rectangle drawing element has been added to canvas
        }

        [Test]
        public void ShouldDrawLineOnCanvas()
        {
            //Given
            PaintingMediator paint = new PaintingMediator();
            paint.ChangeTool(eTool.Line);

            //When
            //Triggered MouseUp event

            //Then
            //Line drawing element has been added to canvas
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
            Assert.AreEqual(expectedSize, paint.GetCanvasSize);
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
        }
    }
}
