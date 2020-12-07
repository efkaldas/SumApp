using Microsoft.VisualStudio.TestTools.UnitTesting;
using SumApp;
using System.Collections.Generic;

namespace TestSumApp
{
    [TestClass]
    public class TestSum
    {
        [TestMethod]
        public void TestSumNumbers()
        {
            List<int> numbers = new List<int> { 5, 6, 10, 15 };
            var sum = Program.SumNumbers(numbers);
            var expectedSum = 36;

            Assert.AreEqual(expectedSum, sum);
        }
    }
}
