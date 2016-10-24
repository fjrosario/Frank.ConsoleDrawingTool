using System;
using Huge.DrawingTool.Domain.Commands;
using System.Collections.Generic;
using System.Linq;
using Huge.DrawingTool.Domain.Entities;

namespace Huge.DrawingTool.Domain.Helpers
{
    public class CommandParserHelper
    {

        public CommandParserHelper()
        {
        }
        const char DEFAULT_PARAM_DELIMITER = ' ';
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandLines"></param>
        /// <returns></returns>
        public static IEnumerable<string> SanitizeInput(IEnumerable<string> commandLines)
        {
            if (commandLines == null)
            {
                throw new ArgumentNullException(nameof(commandLines));
            }
            const string WHITE_SPACE_MATCH_REGEX = @"\s+";
            var regEx = new System.Text.RegularExpressions.Regex(WHITE_SPACE_MATCH_REGEX);

            var sanitizedCommandLines = 
                commandLines
                //trim any excess white space between parameters
                .Select(s => regEx.Replace(s.Trim(), DEFAULT_PARAM_DELIMITER.ToString()))
                //Remove any empty strings
                .Where(s => s != string.Empty && s.StartsWith("//") == false);

            return sanitizedCommandLines;
        }

        public static ICommand GetCommandFromCommandLine(ExecutionContext ctx, string commandLine)
        {
            string[] clParts = commandLine.Split(DEFAULT_PARAM_DELIMITER).ToArray();
            string commandName = clParts[0].ToUpper();
            IEnumerable<string> args = clParts.Skip(1);

            var cmd = Factories.CommandFactory.CreateCommand(commandName, ctx, args);
            return cmd;
        }

    }
}
