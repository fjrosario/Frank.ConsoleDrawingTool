using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frank.ConsoleDrawingTool.Domain.Entities;
using Frank.ConsoleDrawingTool.Domain.Helpers;

namespace Frank.ConsoleDrawingTool.Domain.Commands
{
    public class DrawRectangleCommand: BaseCommand, ICommand
    {
        public string CommandHelpMessage
        {
            get
            {
                return string.Format("Usage: {0} <x1> <y1> <x2> <y2>", CommandName);
            }
        }

        public void Execute()
        {
            if (_canvasContext.Canvas == null)
            {
                throw new Exception("Error: Canvas has not been initialized");
            }

            if (_canvasContext.Canvas.IsPointOnCanvas(X1, Y1) == false)
            {
                throw new ArgumentOutOfRangeException(string.Format("Error: Starting point ({0},{1}) not on canvas.", X1, Y1));
            }

            if (_canvasContext.Canvas.IsPointOnCanvas(X2, Y2) == false)
            {
                throw new ArgumentOutOfRangeException(string.Format("Error: Ending point ({0},{1}) not on canvas.", X2, Y2));
            }

            _canvasContext.Canvas.DrawRectangle(X1, Y1, X2, Y2);

        }

        public static string CommandName
        {
            get
            {
                return "R";
            }
        }

        public int X1 { get; private set; }
        public int Y1 { get; private set; }
        public int X2 { get; private set; }
        public int Y2 { get; private set; }

        public DrawRectangleCommand(ExecutionContext ctx, IEnumerable<string> args) : base(ctx)
        {
            this.ParseArguments(args);
        }


        public void ParseArguments(IEnumerable<string> args)
        {
            this.ParseArguments(args.ToArray());
        }

        public void ParseArguments(string[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            var argsArray = args.ToArray();
            this.ValidateArgumentCount(args);

            int x = ValidationHelper.ValidateAndParseInt(args[0]);
            int y = ValidationHelper.ValidateAndParseInt(args[1]);


            X1 = x;
            Y1 = y;

            x = ValidationHelper.ValidateAndParseInt(args[2]);
            y = ValidationHelper.ValidateAndParseInt(args[3]);

            X2 = x;
            Y2 = y;

        }

        public override int ArgumentCount { get { return 4; } }
    }
}

