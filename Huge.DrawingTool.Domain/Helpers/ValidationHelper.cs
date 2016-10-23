using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huge.DrawingTool.Domain.Helpers
{
    public class ValidationHelper
    {
        public static int ValidateAndParseInt(string str)
        {
            int value = 0;
            bool parseSuccess = Int32.TryParse(str, out value);

            if (parseSuccess == false || value < 1)
            {
                throw new ArgumentException("Value must be valid integer greater than or equal to 1", nameof(value));
            }

            //adjust coordinates for Canvas, as command lines are 1 array based and canvas coordinate system is zero array based.

            return value;
        }
    }
}
