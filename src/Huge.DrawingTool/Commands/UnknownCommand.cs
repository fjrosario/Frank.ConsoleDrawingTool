using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huge.DrawingTool.Commands
{
    public class UnknownCommand : ICommand
    {
        public void Execute()
        {
            Console.Error.WriteLine("Error: unknown command");
        }
    }
}
