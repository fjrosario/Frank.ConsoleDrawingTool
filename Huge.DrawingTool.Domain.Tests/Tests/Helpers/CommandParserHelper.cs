using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huge.DrawingTool.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Huge.DrawingTool.Domain.Tests.Tests.Helpers
{
    [TestClass]
    public class CommandParserHelper
    {
        protected const string CREATE_CANVAS_COMMAND_TEXT = "C 20 4";
        protected const string DRAW_LINE_COMMAND_TEXT = "L 1 2 6 2";
        protected const string DRAW_RECTANGLE_COMMAND_TEXT = "R 16 1 20 3";
        protected const string FILL_COMMAND_TEXT = "B 10 3 o";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SanitizeInput_NullTest()
        {
            Domain.Helpers.CommandParserHelper.SanitizeInput(null);
        }

        [TestMethod]
        public void SanitizeInput_EmptyTest()
        {
            var results = Domain.Helpers.CommandParserHelper.SanitizeInput(Enumerable.Empty<string>());

            Assert.IsFalse(results.Any());
        }

        [TestMethod]
        public void SanitizeInput_ValidInputTest()
        {
            string[] testInput = new string[]
            {
                CREATE_CANVAS_COMMAND_TEXT,
                DRAW_LINE_COMMAND_TEXT,
                DRAW_RECTANGLE_COMMAND_TEXT
            };
            var results = Domain.Helpers.CommandParserHelper.SanitizeInput(testInput).ToArray();

            var testResults = Domain.Tests.Helpers.ComparisonHelper.ArraysAreEqual(results, testInput);

            Assert.IsTrue(testResults);
        }




        [TestMethod]
        public void GetCommandFromCommandLine_CreateCanvas()
        {
            Domain.Entities.ExecutionContext ctx = new ExecutionContext();
            var cmd = Domain.Helpers.CommandParserHelper.GetCommandFromCommandLine(ctx, CREATE_CANVAS_COMMAND_TEXT);
            Assert.IsInstanceOfType(cmd, typeof(Domain.Commands.CreateCanvasCommand));
        }

        [TestMethod]
        public void GetCommandFromCommandLine_DrawLine()
        {
            var ctx = Factories.CanvasFactory.CreatExecutionContextWithCanvas();
            var cmd = Domain.Helpers.CommandParserHelper.GetCommandFromCommandLine(ctx, DRAW_LINE_COMMAND_TEXT);
            Assert.IsInstanceOfType(cmd, typeof(Domain.Commands.DrawLineCommand));
        }

        [TestMethod]
        public void GetCommandFromCommandLine_DrawRectangle()
        {
            var ctx = Factories.CanvasFactory.CreatExecutionContextWithCanvas();
            var cmd = Domain.Helpers.CommandParserHelper.GetCommandFromCommandLine(ctx, DRAW_RECTANGLE_COMMAND_TEXT);
            Assert.IsInstanceOfType(cmd, typeof(Domain.Commands.DrawRectangleCommand));
        }

        [TestMethod]
        public void GetCommandFromCommandLine_FillCommand()
        {
            var ctx = Factories.CanvasFactory.CreatExecutionContextWithCanvas();
            var cmd = Domain.Helpers.CommandParserHelper.GetCommandFromCommandLine(ctx, FILL_COMMAND_TEXT);
            Assert.IsInstanceOfType(cmd, typeof(Domain.Commands.FillCommand));
        }


    }
}
