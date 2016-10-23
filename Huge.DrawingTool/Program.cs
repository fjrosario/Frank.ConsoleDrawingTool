using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huge.DrawingTool.Commands;
using Huge.DrawingTool.Entities;
using Huge.DrawingTool.Services;

namespace Huge.DrawingTool
{
    public class Program
    {

        public static void LogError(string errMsg)
        {
            Console.Error.WriteLine(errMsg);
        }

        public static void Main(string[] args)
        {
            if(args.Count() != 2)
            {
                LogError(@"Error: command line arguments incorrect. Format: DT c:\path\to\input.text c:\path\to\output.txt");
                return;
            }

            string fileInputPath = args[0];
            string fileOutputPath = args[1];

            //check if file input exists

            if (System.IO.File.Exists(fileInputPath) == false)
            {
                LogError(string.Format(@"Error: '{0}' could not be found", fileInputPath));
                return;
            }


            //process input
            var commandTextLines = System.IO.File.ReadAllLines(fileInputPath);
            //  cleanup input
            var sanitizedCommandLines = CommandParserService.SanitizeInput(commandTextLines);
            //  extract commands from input
            var ctx = new ExecutionContext();
            var commands = sanitizedCommandLines.Select(s => CommandParserService.GetCommandFromCommandLine(ctx, s)).ToList();

            //make sure first command is creating Canvas

            if (commands.Any())
            {
                //ensure first command is creating Canvas
                if ((commands.First() is CreateCanvasCommand) == false)
                {
                    LogError("Error: first command must to create canvas not specified");
                    return;
                }
                foreach (var command in commands)
                {
                    command.Execute();
                }

            }
            else
            {
                LogError("Error: no commands specified in input file.");
                return;
            }
        }
    }
}
