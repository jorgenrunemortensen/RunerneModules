using Microsoft.VisualStudio.TestTools.UnitTesting;
using Runerne.Computation.Numeric;

namespace Runerne.Computation.UnitTest
{
    [TestClass]
    public class CompareTest
    {
        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("Compare")]
        public void TestNumericEquals()
        {
            {
                var compare = new NumericEquals(
                    new NumericConstant(4),
                    new NumericConstant(4)
                    );
                Assert.AreEqual(true, compare.Value);
            }
            {
                var compare = new NumericEquals(
                    new NumericConstant(4),
                    new NumericConstant(5),
                    new NumericConstant(4)
                    );
                Assert.AreEqual(false, compare.Value);
            }
        }
    }
}
