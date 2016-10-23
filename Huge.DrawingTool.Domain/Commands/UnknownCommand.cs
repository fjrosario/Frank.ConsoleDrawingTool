﻿using System;

namespace Huge.DrawingTool.Domain.Commands
{
    public class UnknownCommand : ICommand
    {
        public void Execute()
        {
            Console.Error.WriteLine("Error: unknown command");
        }
    }
}