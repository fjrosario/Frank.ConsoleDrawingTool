using System;
using System.Collections.Generic;
using System.Linq;
using Huge.DrawingTool.Commands;
using Huge.DrawingTool.Entities;

namespace Huge.DrawingTool.Factories
{
    public static class CommandFactory
    {
        private static readonly IDictionary<string, Func<ExecutionContext, IEnumerable<string>, ICommand>> availableCommands;

        static CommandFactory()
        {
            availableCommands = new Dictionary<string, Func<ExecutionContext, IEnumerable<string>, ICommand>>();

            availableCommands.Add(CreateCanvasCommand.CommandName, (ctx, args) => new CreateCanvasCommand(ctx, args));
            availableCommands.Add(DrawLineCommand.CommandName, (ctx, args) => new DrawLineCommand(ctx, args));
            availableCommands.Add(DrawRectangleCommand.CommandName, (ctx, args) => new DrawRectangleCommand(ctx, args));
            availableCommands.Add(FillCommand.CommandName, (ctx, args) => new FillCommand(ctx, args));
        }

        public static ICommand CreateCommand(string cmdStr, ExecutionContext ctx, IEnumerable<string> args)
        {
            if (availableCommands.ContainsKey(cmdStr))
            {
                return availableCommands[cmdStr](ctx, args);
            }
            //couldn't find command, add somewhere else
            else
            {
                return new UnknownCommand();
            }
        }
    }
}
