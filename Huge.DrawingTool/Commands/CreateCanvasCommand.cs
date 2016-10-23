using System;
using System.Collections.Generic;
using System.Linq;
using Huge.DrawingTool.Entities;

namespace Huge.DrawingTool.Commands
{
    public class CreateCanvasCommand : BaseCommand, ICommand
    {
        public static string CommandHelpMessage
        {
            get
            {
                return string.Format("Usage: {0} <height> <width>", CommandName);
            }
        }

        public override int ArgumentCount { get { return 2; } }

        public void Execute()
        {
            this._canvasContext.Canvas = new Canvas(this.Height, this.Width);
        }

        public static string CommandName
        {
            get
            {
                return "C";
            }
        }

        public int Height { get; private set; }
        public int Width { get; private set; }

        public CreateCanvasCommand(ExecutionContext ctx, IEnumerable<string> args): base(ctx)
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

            

            int height = Helpers.ValidationHelper.ValidateAndParseInt(args.First());
            if (height < Canvas.MIN_HEIGHT)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            this.Height = height;

            int width = Helpers.ValidationHelper.ValidateAndParseInt(args[1]);

            if (width < Canvas.MIN_WIDTH)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            this.Width = width;


        }

    }
}
