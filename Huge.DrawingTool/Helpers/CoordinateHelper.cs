using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huge.DrawingTool.Helpers
{
    public static class CoordinateHelper
    {
        public static int AdjustCoordinate(int coordinate)
        {
            return coordinate - 1;
        }

        public static int GetCoordinateFromString(string coordinate)
        {
            int validInt = Helpers.ValidationHelper.ValidateAndParseInt(coordinate);
            int adjustedInt = AdjustCoordinate(validInt);
            return adjustedInt;
        }
    }
}
