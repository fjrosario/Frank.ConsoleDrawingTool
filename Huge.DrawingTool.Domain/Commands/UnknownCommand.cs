using System;

namespace Huge.DrawingTool.Domain.Commands
{
    public class UnknownCommand : ICommand
    {
        public void Execute()
        {
            throw new Exception("Error: unknown command");
        }
    }
}
