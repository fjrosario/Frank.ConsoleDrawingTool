using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frank.ConsoleDrawingTool.Domain.Tests.Helpers
{
    public static class ComparisonHelper
    {
        public static bool ArraysAreEqual(object[] arr1, object[] arr2)
        {
            //this will only work evaluate to true if both are null
            if (arr1 == arr2)
            {
                return true;
            }

            if (arr1.Length != arr2.Length)
            {
                return false;
            }

            int len = arr1.Length;

            for (int x = 0; x < len; x++)
            {
                if (arr1[x].Equals(arr2[x]) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
