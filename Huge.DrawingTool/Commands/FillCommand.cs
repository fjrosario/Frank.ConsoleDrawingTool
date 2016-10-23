using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huge.DrawingTool.Entities;

namespace Huge.DrawingTool.Commands
{
    public class FillCommand: BaseCommand, ICommand
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

            var targetColor = _canvasContext.Canvas.GetUnit(X, Y);
            _canvasContext.Canvas.FloodFill(X,Y, targetColor, ReplacementColor);

        }

        public static string CommandName
        {
            get
            {
                return "B";
            }
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public char ReplacementColor { get; private set; }

        public FillCommand(ExecutionContext ctx, IEnumerable<string> args) : base(ctx)
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

            int x = Helpers.ValidationHelper.ValidateAndParseInt(args[0]) - 1;
            int y = Helpers.ValidationHelper.ValidateAndParseInt(args[1]) - 1;

            if (_canvasContext.Canvas.IsPointOnCanvas(x, y) == false)
            {
                throw new ArgumentOutOfRangeException(string.Format("Error: Fill start point ({0},{1}) not on canvas.", x, y));
            }
            X = x;
            Y = y;


            this.ReplacementColor = args[2][0];

        }

        public override int ArgumentCount { get { return 3; } }
    }
}

