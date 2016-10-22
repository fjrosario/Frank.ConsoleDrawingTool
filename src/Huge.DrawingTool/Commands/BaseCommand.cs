using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huge.DrawingTool.Commands
{
    public abstract class BaseCommand
    {
        abstract void ParseArguments(IEnumerable<string> args);
    }
}
