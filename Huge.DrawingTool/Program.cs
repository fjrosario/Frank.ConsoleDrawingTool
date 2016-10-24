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

        const int DEFAULT_ERROR_EXIT_CODE = -255;

        public static void LogError(string errMsg)
        {
            Console.Error.WriteLine(errMsg);
            Exit();
        }

        public static void PrintChar(char c)
        {
            Console.Out.Write(c);
        }

        public static void Print(string msg)
        {
            Console.Out.WriteLine(msg);
        }

        public static void Exit(int exitCode = DEFAULT_ERROR_EXIT_CODE)
        {
            Console.ReadLine();
            Environment.Exit(exitCode);
        }

        public static void Main(string[] args)
        {
            if(args.Count() != 2)
            {
                LogError(@"Error: command line arguments incorrect. Format: Huge.DrawingTool c:\path\to\input.txt c:\path\to\output.txt");
            }

            string fileInputPath = args[0];
            string fileOutputPath = args[1];

            //check if file input exists

            if (System.IO.File.Exists(fileInputPath) == false)
            {
                LogError(string.Format(@"Error: '{0}' could not be found", fileInputPath));
            }

            try
            {
                //process input
                var commandTextLines = System.IO.File.ReadAllLines(fileInputPath);
                //  cleanup input
                var sanitizedCommandLines = CommandParserHelper.SanitizeInput(commandTextLines).ToList();
                var ctx = new ExecutionContext();

                var commands =
                    sanitizedCommandLines.Select(s => CommandParserHelper.GetCommandFromCommandLine(ctx, s)).ToList();

                if (commands.Any() == false)
                {
                    LogError(string.Format("Error: no commands to execute in file '{0}'", fileInputPath));
                }

                //  extract commands from input
                //make sure first command is creating Canvas
                IList<string> outputScreens = new List<string>();

                var firstCmd = commands.FirstOrDefault();

                if ((firstCmd is CreateCanvasCommand) == false)
                {
                    LogError("Error: first command must be to create canvas");
                    Console.ReadLine();
                }

                //Write to file

                string gfxBufferStr = null;

                foreach (var command in commands)
                {
                    command.Execute();
                    gfxBufferStr = ctx.Canvas.DumpBufferToString();
                    outputScreens.Add(gfxBufferStr);
                    Console.WriteLine(gfxBufferStr);
                }

                System.IO.File.WriteAllText(fileOutputPath, string.Join(Environment.NewLine, outputScreens));
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }



        }
    }
}
