using System;
using System.Collections.Generic;
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
        public const char HORIZONTAL_LINE_FILL = '-';
        public const char VERTICAL_LINE_FILL = '|';

        private char[,] _canvasArray;
        private readonly int _height = 0;
        private readonly int _width = 0;
        private readonly int _heightMax = 0;
        private readonly int _widthMax = 0;

        public bool IsPointOnCanvas(int x, int y)
        {
            if(x <= X_ORIGIN || y <= Y_ORIGIN)
            {
                return false;
            }

            if (_canvasArray == null)
            {
                throw new Exception(string.Format("Internal error, {0} is null", nameof(_canvasArray)));
            }

            return x < _width && y < _height;
        }

        public Canvas(int height, int width)
        {
            if(height < MIN_HEIGHT)
            {
                throw new ArgumentOutOfRangeException(nameof(height), string.Format("Height parameter must be at {0} or greater", MIN_HEIGHT);
            }
            if (width < MIN_WIDTH)
            {
                throw new ArgumentOutOfRangeException(nameof(width), string.Format("Width parameter must be at {0} or greater", MIN_HEIGHT);
            }

            _canvasArray = new char[width, height];
            _width = width;
            _height = height;
            _widthMax = _width - 1;
            _heightMax = _height - 1;
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


            int rise = (y2 - y1);
            int run = (x2 - x1);
            int slope = rise / run;
            bool descendingX = run < 0;
            int xIncrement = descendingX ? -1 : 1;

            for(int curX = x1; curX <= x2; curX += xIncrement)
            {
                int curY = y1 + (int)Math.Round(((decimal)(slope * curX)));
                _canvasArray[curX, curY] = color;
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

        private void InitialFill()
        {
            for(int x=0; x < _width; x++)
            {
                for (int y=0; y < _height; y++)
                {
                    _canvasArray[x, y] = DEFAULT_FILL;
                }
            }
        }

        private void DrawBorders()
        {
            //Draw top and bottom borders
            this.DrawLine(X_ORIGIN, Y_ORIGIN, _widthMax, Y_ORIGIN, HORIZONTAL_LINE_FILL);
            this.DrawLine(X_ORIGIN, _heightMax, _widthMax, _heightMax, HORIZONTAL_LINE_FILL);
            //Draw left/right borders
            this.DrawLine(X_ORIGIN, Y_ORIGIN + 1, X_ORIGIN, _heightMax - 1,VERTICAL_LINE_FILL);
            this.DrawLine(_widthMax, Y_ORIGIN + 1, _widthMax, _heightMax - 1,VERTICAL_LINE_FILL);
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

    }
}
