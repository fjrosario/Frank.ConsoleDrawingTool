using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huge.DrawingTool.Domain.Entities
{
    public class Canvas
    {
        public const int MIN_HEIGHT = 1;
        public const int MIN_WIDTH = 1;
        public const int X_ORIGIN = 0;
        public const int Y_ORIGIN = 0;
        public const char DEFAULT_COLOR = 'X';
        public const char DEFAULT_FILL_COLOR = ' ';
        public const char HORIZONTAL_BORDER_LINE_FILL = '-';
        public const char VERTICAL_BORDER_LINE_FILL = '|';

        private readonly char[,] _canvasArray;
        private readonly int _actualWidth;
        private readonly int _actualHeight;
        
        public char DefaultFill { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        /// <summary>
        /// Test if point is exists on canvas or is out of bounds
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
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

            return x < _actualWidth && y < _actualHeight;
        }

        public Canvas(int height, int width, char defaultFill = DEFAULT_FILL_COLOR)
        {
            if(height < MIN_HEIGHT)
            {
                throw new ArgumentOutOfRangeException(nameof(height), string.Format("Height parameter must be at least {0} or greater", MIN_HEIGHT));
            }
            if (width < MIN_WIDTH)
            {
                throw new ArgumentOutOfRangeException(nameof(width), string.Format("Width parameter must be at least {0} or greater", MIN_HEIGHT));
            }

            //add two to each dimension for borders
            _canvasArray = new char[height + 2, width + 2];
            Width = width;
            Height = height;
            DefaultFill = defaultFill;

            //internal fields to story actual dimensions of array
            //Width/Height properties only report dimensions not including borders
            _actualWidth = _canvasArray.GetLength(1);
            _actualHeight = _canvasArray.GetLength(0);

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
            //Draw top and bottom lines
            this.DrawLine(x1, y1, x2, y1, color);
            this.DrawLine(x1, y2, x2, y2, color);
            //Draw left/right lines
            this.DrawLine(x1, y1, x1, y2, color);
            this.DrawLine(x2, y1, x2, y2, color);
        }

        /// <summary>
        /// 
        /// Retrieves value of unit as defined by x and y.
        /// 
        /// Note:
        /// This is called GetUnit instead of GetColor because I wanted the solution
        /// to be scalable incase we wanted to change the atomic unit of the canvas
        /// from something besides a char
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public char GetUnit(int x, int y)
        {
            if (this.IsPointOnCanvas(x, y) == false)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _canvasArray[y, x];
        }

        /// <summary>
        /// 
        /// Retrieves value of unit as defined by x and y.
        /// 
        /// Note:
        /// This is called DrawUnit instead of DrawColor because I wanted the solution
        /// to be scalable incase we wanted to change the atomic unit of the canvas
        /// from something besides a char
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="unit">value to fill unit with</param>
        public void DrawUnit(int x, int y, char unit)
        {
            if (this.IsPointOnCanvas(x, y) == false)
            {
                throw new ArgumentOutOfRangeException();
            }

            _canvasArray[y, x] = unit;
        }

        private void InitialFill()
        {
            for(int x=0; x < _actualWidth; x++)
            {
                for (int y=0; y < _actualHeight; y++)
                {
                    this.DrawUnit(x, y, DefaultFill);
                }
            }
        }

        private void DrawBorders()
        {
            int widthMax = this._actualWidth - 1;
            int heightMax = this._actualHeight - 1;
            //Draw top and bottom borders
            this.DrawLine(X_ORIGIN, Y_ORIGIN, widthMax, Y_ORIGIN, HORIZONTAL_BORDER_LINE_FILL);
            this.DrawLine(X_ORIGIN, heightMax, widthMax, heightMax, HORIZONTAL_BORDER_LINE_FILL);
            //Draw left/right borders
            //Y_ORIGIN + 1 and heightMax - 1 so we don't draw over the ends of the horizontal borders
            this.DrawLine(X_ORIGIN, Y_ORIGIN + 1, X_ORIGIN, heightMax - 1, VERTICAL_BORDER_LINE_FILL);
            this.DrawLine(widthMax, Y_ORIGIN + 1, widthMax, heightMax - 1, VERTICAL_BORDER_LINE_FILL);
        }



        public void Initialize()
        {
            this.InitialFill();
            this.DrawBorders();
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
        /// <param name="targetColor">target color to replace</param>
        /// <param name="replacementColor">replacement color for target</param>
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
            // cloning internal array. Don't want to return a direct reference to the internal array,
            // otherwise a client could manipulate the _canvasArray outside of the class's methods.
            return (char[,])_canvasArray.Clone();
        }

        public string DumpBufferToString()
        {
            var gfxBuffer = DumpBuffer();
            var sbOutput = new StringBuilder();

            for (int y = 0; y < _actualHeight; y++)
            {
                for (int x = 0; x < _actualWidth; x++)
                {
                    char c = gfxBuffer[y, x];
                    sbOutput.Append(c);
                }
                sbOutput.AppendLine();
            }

            return sbOutput.ToString().Trim();
        }
    }
}
