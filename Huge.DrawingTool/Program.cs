using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Huge.DrawingTool.Domain.Commands;
using Huge.DrawingTool.Domain.Entities;
using Huge.DrawingTool.Domain.Helpers;

namespace Huge.DrawingTool
{
    public class Program
    {

        public static void LogError(string errMsg)
        {
            Console.Error.WriteLine(errMsg);
        }

        public static void PrintChar(char c)
        {
            Console.Out.Write(c);
        }

        public static void Print(string msg)
        {
            Console.Out.WriteLine(msg);
        }

        public static void Main(string[] args)
        {
            if(args.Count() != 2)
            {
                LogError(@"Error: command line arguments incorrect. Format: Huge.DrawingTool c:\path\to\input.text c:\path\to\output.txt");
                Console.ReadLine();
                return;
            }

            string fileInputPath = args[0];
            string fileOutputPath = args[1];

            //check if file input exists

            if (System.IO.File.Exists(fileInputPath) == false)
            {
                LogError(string.Format(@"Error: '{0}' could not be found", fileInputPath));
                Console.ReadLine();
                return;
            }
            


            //process input
            var commandTextLines = System.IO.File.ReadAllLines(fileInputPath);
            //  cleanup input
            var sanitizedCommandLines = CommandParserHelper.SanitizeInput(commandTextLines).ToList();
            var ctx = new ExecutionContext();

            //  extract commands from input
            //make sure first command is creating Canvas
            IList<string> outputScreens = new List<string>();
            var firstCmd = CommandParserHelper.GetCommandFromCommandLine(ctx, sanitizedCommandLines.First());
            if ((firstCmd is CreateCanvasCommand) == false)
            {
                LogError("Error: first command must be to create canvas");
                Console.ReadLine();
            }
            firstCmd.Execute();
            var gfxBufferStr = ctx.Canvas.DumpBufferToString();
            outputScreens.Add(gfxBufferStr);
            Console.WriteLine(gfxBufferStr);

            var remainderCommands = sanitizedCommandLines.Skip(1).Select(s => CommandParserHelper.GetCommandFromCommandLine(ctx, s)).ToList();

            //Write to file

            foreach (var command in remainderCommands)
            {
                command.Execute();
                gfxBufferStr = ctx.Canvas.DumpBufferToString();
                outputScreens.Add(gfxBufferStr);
                Console.WriteLine(gfxBufferStr);
            }

            System.IO.File.WriteAllText(fileOutputPath, string.Join(Environment.NewLine,outputScreens));



            Console.ReadLine();
        }
    }
}
