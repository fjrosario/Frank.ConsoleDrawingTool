using System;

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

            return value;
        }
    }
}
