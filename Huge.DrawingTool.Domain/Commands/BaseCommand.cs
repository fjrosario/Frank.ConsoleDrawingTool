using System;
using System.Collections.Generic;
using System.Linq;
using Huge.DrawingTool.Domain.Entities;

namespace Huge.DrawingTool.Domain.Commands
{
    public abstract class BaseCommand
    {
        protected ExecutionContext _canvasContext;

        public abstract int ArgumentCount { get; }

        public BaseCommand(ExecutionContext ctx)
        {
            if (ctx == null)
            {
                throw new ArgumentNullException(nameof(ctx));
            }
            _canvasContext = ctx;
        }

        public void ValidateArgumentCount(IEnumerable<string> args)
        {
            if (args.Count() < this.ArgumentCount)
            {
                throw new Exception("Error: invalid number of args");
            }
        }
    }
}
