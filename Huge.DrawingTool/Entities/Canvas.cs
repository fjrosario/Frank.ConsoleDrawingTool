﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Huge.DrawingTool.Entities
{
    public class Canvas
    {
        public const int MIN_HEIGHT = 1;
        public const int MIN_WIDTH = 1;
        public const int X_ORIGIN = 0;
        public const int Y_ORIGIN = 0;
        public const char DEFAULT_COLOR = 'X';
        public const char DEFAULT_FILL = ' ';
        public const char HORIZONTAL_BORDER_LINE_FILL = '-';
        public const char VERTICAL_BORDER_LINE_FILL = '|';

        private readonly char[,] _canvasArray;
        public int Height { get; private set; }
        public int Width { get; private set; }

        public bool IsPointOnCanvas(int x, int y)
        {
            if(x < X_ORIGIN || y < Y_ORIGIN)
            {
                return false;
            }

            if (_canvasArray == null)
            {
                throw new Exception(string.Format("Internal error, {0} is null", nameof(_canvasArray)));
            }

            return x < Width && y < Height;
        }

        public Canvas(int height, int width)
        {
            if(height < MIN_HEIGHT)
            {
                throw new ArgumentOutOfRangeException(nameof(height), string.Format("Height parameter must be at {0} or greater", MIN_HEIGHT));
            }
            if (width < MIN_WIDTH)
            {
                throw new ArgumentOutOfRangeException(nameof(width), string.Format("Width parameter must be at {0} or greater", MIN_HEIGHT));
            }

            //add two to each dimension for border
            _canvasArray = new char[height, width];
            Width = width;
            Height = height;
            this.Initialize();
        }

        public void DrawLine(int x1, int y1, int x2, int y2, char color = DEFAULT_COLOR)
        {

            if(this.IsPointOnCanvas(x1,y1) == false)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (this.IsPointOnCanvas(x2, y2) == false)
            {
                throw new ArgumentOutOfRangeException();
            }

            int dx = x2 - x1;
            int dy = y2 - y1;

            int absDx = Math.Abs(dx);
            int absDy = Math.Abs(dy);

            int steps = Math.Max(absDx,absDy);

            float xIncrement = (float)dx/steps;
            float yIncrement = (float)dy/steps;

            float curX = x1;
            float curY = y1;
            
            for(int cntr=0; cntr <= steps; cntr++)
            {
                int intCurX = (int)Math.Round(curX);
                int intCurY = (int)Math.Round(curY);

                this.DrawUnit(intCurX, intCurY, color);

                curX += xIncrement;
                curY += yIncrement;
            }

        }

        public void DrawRectangle(int x1, int y1, int x2, int y2, char color = DEFAULT_COLOR)
        {
            //Draw top and bottom borders
            this.DrawLine(x1, y1, x2, y1, color);
            this.DrawLine(x1, y2, x2, y2, color);
            //Draw left/right borders
            this.DrawLine(x1, y1, x1, y2, color);
            this.DrawLine(x2, y1, x2, y2, color);
        }

        public char GetUnit(int x, int y)
        {
            if (this.IsPointOnCanvas(x, y) == false)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _canvasArray[y, x];
        }

        public void DrawUnit(int x, int y, char color)
        {
            if (this.IsPointOnCanvas(x, y) == false)
            {
                throw new ArgumentOutOfRangeException();
            }

            _canvasArray[y, x] = color;
        }

        private void InitialFill(char fill = 'S')
        {
            for(int x=0; x < Width; x++)
            {
                for (int y=0; y < Height; y++)
                {
                    this.DrawUnit(x, y, fill);
                }
            }
        }

        private void DrawBorders()
        {
            int widthMax = this.Width - 1;
            int heightMax = this.Height - 1;
            //Draw top and bottom borders
            this.DrawLine(X_ORIGIN, Y_ORIGIN, widthMax, Y_ORIGIN, HORIZONTAL_BORDER_LINE_FILL);
            this.DrawLine(X_ORIGIN, heightMax, widthMax, heightMax, HORIZONTAL_BORDER_LINE_FILL);
            //Draw left/right borders
            //heightMax - 1 so we don't draw over the ends of the horizontal borders
            this.DrawLine(X_ORIGIN, Y_ORIGIN + 1, X_ORIGIN, heightMax, VERTICAL_BORDER_LINE_FILL);
            this.DrawLine(widthMax - 1, Y_ORIGIN + 1, widthMax - 1, heightMax, VERTICAL_BORDER_LINE_FILL);
        }



        public void Initialize()
        {
            this.InitialFill();
            //this.DrawBorders();
        }

        public void Reset()
        {
            this.Initialize();
        }

        /// <summary>
        ///  Flood fill algorithm, pulled from Wikipedia:
        ///  https://en.wikipedia.org/wiki/Flood_fill#Stack-based_recursive_implementation_.28four-way.29
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="targetColor"></param>
        /// <param name="replacementColor"></param>
        public void FloodFill(int x, int y, char targetColor, char replacementColor)
        {
            if(this.IsPointOnCanvas(x,y) == false)
            {
                return;
            }
            char curColor = this.GetUnit(x, y);

            //1.If target - color is equal to replacement-color, return.
            if (curColor == replacementColor)
            {
                return;
            }

            //2. If the color of node is not equal to target-color, return.
            if(curColor != targetColor)
            {
                return;
            }

            //3.Set the color of node to replacement-color.
            this.DrawUnit(x, y, replacementColor);

            //Perform Flood-fill(one step to the south of node, target - color, replacement - color).
            this.FloodFill(x, y + 1, targetColor, replacementColor);
            //Perform Flood - fill(one step to the north of node, target - color, replacement - color).
            this.FloodFill(x, y - 1, targetColor, replacementColor);
            //Perform Flood - fill(one step to the west of node, target - color, replacement - color).
            this.FloodFill(x - 1, y, targetColor, replacementColor);
            //Perform Flood - fill(one step to the east of node, target - color, replacement - color).
            this.FloodFill(x + 1, y, targetColor, replacementColor);

            return;
        }

        public char[,] DumpBuffer()
        {
            return _canvasArray;
        }
    }
}
