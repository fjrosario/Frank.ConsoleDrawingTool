using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huge.DrawingTool.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Huge.DrawingTool.Domain.Tests.Tests.Entities
{
    [TestClass]
    public class CanvasTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateCanvasBelowMinimumHeightTest()
        {
            var c = new Canvas(Canvas.MIN_HEIGHT - 1, Canvas.MIN_WIDTH);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateCanvasBelowMinimumWidthTest()
        {
            var c = new Canvas(Canvas.MIN_HEIGHT, Canvas.MIN_WIDTH - 1);
        }

        [TestMethod]
        public void CreateStandardCanvasTest()
        {
            const int HEIGHT = 5;
            const int WIDTH = 20;
            var c = new Canvas(HEIGHT, WIDTH);

            Assert.AreEqual(c.Height, HEIGHT);
            Assert.AreEqual(c.Width, WIDTH);
        }



        [TestMethod]
        public void DrawCharTest()
        {
            const int HEIGHT = 1;
            const int WIDTH = 1;

            const string TEST_PATTERN = "---\r\n|X|\r\n---";

            var c = new Canvas(HEIGHT, WIDTH);

            //Draw VerticalLine
            c.DrawUnit(1, 1, 'X');

            var bufferString = c.DumpBufferToString();

            Assert.AreEqual(bufferString, TEST_PATTERN);
        }


        [TestMethod]
        public void DrawLineTest()
        {
            const int HEIGHT = 3;
            const int WIDTH = 5;

            const string TEST_PATTERN = "-------\r\n|     |\r\n|XXXXX|\r\n|     |\r\n-------";

            var c = new Canvas(HEIGHT, WIDTH);

            //Draw Horizontal Align across canvas
            c.DrawLine(1,2,WIDTH,2);

            var bufferString = c.DumpBufferToString();

            Assert.AreEqual(bufferString, TEST_PATTERN);
        }

        [TestMethod]
        public void DrawRectangleTest()
        {
            const int HEIGHT = 3;
            const int WIDTH = 5;

            const string TEST_PATTERN = "-------\r\n|XXXXX|\r\n|X   X|\r\n|XXXXX|\r\n-------";

            var c = new Canvas(HEIGHT, WIDTH);

            //Draw Rectangle
            c.DrawRectangle(1, 1, WIDTH, HEIGHT);

            var bufferString = c.DumpBufferToString();

            Assert.AreEqual(bufferString, TEST_PATTERN);
        }

        [TestMethod]
        public void FillTest()
        {
            const int HEIGHT = 3;
            const int WIDTH = 5;

            const string TEST_PATTERN = "-------\r\n|Xoooo|\r\n|ooooo|\r\n|ooooX|\r\n-------";

            var c = new Canvas(HEIGHT, WIDTH);

            //Draw Rectangle
            c.DrawUnit(1,1, 'X');
            c.DrawUnit(WIDTH, HEIGHT, 'X');
            var targetColor = c.GetUnit(2, 2);
            c.FloodFill(2,2,targetColor,'o');

            var bufferString = c.DumpBufferToString();

            Assert.AreEqual(bufferString, TEST_PATTERN);
        }


    }
}
