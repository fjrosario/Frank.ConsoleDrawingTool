using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Huge.DrawingTool.Domain.Tests.Tests.Helpers
{
    [TestClass]
    public class ValidationHelper
    {
        [TestMethod]
        public void ValidateAndParseInt_NumericStringTest()
        {
            string numericStr = "5";
            int value = Domain.Helpers.ValidationHelper.ValidateAndParseInt(numericStr);

            Assert.AreEqual(value, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateAndParseInt_alphabetStringTest()
        {
            string testStr = "abcdef";
            int value = Domain.Helpers.ValidationHelper.ValidateAndParseInt(testStr);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateAndParseInt_alphanumerictStringTest()
        {
            string testStr = "123abcdef";
            int value = Domain.Helpers.ValidationHelper.ValidateAndParseInt(testStr);
        }


    }
}
