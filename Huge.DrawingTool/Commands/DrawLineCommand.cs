using System;
using System.Collections.Generic;
using System.Linq;
using Huge.DrawingTool.Entities;

namespace Huge.DrawingTool.Commands
{
    public class DrawLineCommand : BaseCommand, ICommand
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

            _canvasContext.Canvas.DrawLine(X1, Y1, X2, Y2);

        }

        public static string CommandName
        {
            get
            {
                return "L";
            }
        }

        public int X1 { get; private set; }
        public int Y1 { get; private set; }
        public int X2 { get; private set; }
        public int Y2 { get; private set; }

        public DrawLineCommand(ExecutionContext ctx, IEnumerable<string> args) : base(ctx)
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

            int x = Helpers.CoordinateHelper.GetCoordinateFromString(args[0]);
            int y = Helpers.CoordinateHelper.GetCoordinateFromString(args[1]);

            if (_canvasContext.Canvas.IsPointOnCanvas(x, y) == false)
            {
                throw new ArgumentOutOfRangeException(string.Format("Error: Starting point ({0},{1}) not on canvas.",x,y));
            }

            X1 = x;
            Y1 = y;

            x = Helpers.CoordinateHelper.GetCoordinateFromString(args[2]);
            y = Helpers.CoordinateHelper.GetCoordinateFromString(args[3]);

            if (_canvasContext.Canvas.IsPointOnCanvas(x, y) == false)
            {
                throw new ArgumentOutOfRangeException(string.Format("Error: Ending point({0},{1}) not on canvas.", x, y));
            }

            X2 = x;
            Y2 = y;



        }

        public override int ArgumentCount { get { return 4; } }
    }
}
