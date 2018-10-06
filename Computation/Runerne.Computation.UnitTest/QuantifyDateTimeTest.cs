using Microsoft.VisualStudio.TestTools.UnitTesting;
using Runerne.Computation.DateTime;
using Runerne.Computation.Numeric;
using Runerne.Utilities;

namespace Runerne.Computation.UnitTest
{
    [TestClass]
    public class QuantifyDateTimeTest
    {
        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("QuantifyTime")]
        public void TestQuantifyDateTime_Year()
        {
            var quantifyDateTime = new QuantifyDateTime(
                new DateTimeConstant(new System.DateTime(1966, 7, 14)),
                DateTimePart.Years,
                new NumericConstant(2)
            );
            Assert.AreEqual(new System.DateTime(1966, 1, 1), quantifyDateTime.Value);
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("QuantifyTime")]
        public void TestQuantifyDateTime_Month()
        {
            var quantifyDateTime = new QuantifyDateTime(
                new DateTimeConstant(new System.DateTime(1966, 8, 14)),
                DateTimePart.Months,
                new NumericConstant(2)
            );
            Assert.AreEqual(new System.DateTime(1966, 7, 1), quantifyDateTime.Value);
        }
    }
}
