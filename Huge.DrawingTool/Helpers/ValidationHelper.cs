using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huge.DrawingTool.Helpers
{
    public class ValidationHelper
    {
        public static int ValidateAndParseInt(string str)
        {
            int value = 0;
            bool parseSuccess = Int32.TryParse(str, out value);

            if (parseSuccess == false || value < 0)
            {
                throw new ArgumentException("Value must be valid integer greater than or equal to zero", nameof(value));
            }

            return value;
        }
    }
}
