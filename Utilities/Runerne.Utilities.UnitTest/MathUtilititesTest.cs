using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Utilities.UnitTest
{
    [TestClass]
    public class MathUtilititesTest
    {
        [TestMethod]
        [TestCategory("MathUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void DoubleFloor()
        {
            {
                var x = Math.PI;
                var floorX_3 = MathUtilities.Floor(x, 0.001);
                Assert.AreEqual(3.141, floorX_3);
            }

            {
                var x = Math.PI;
                var floorX0 = MathUtilities.Floor(x, 1);
                Assert.AreEqual(3, floorX0);
            }

            {
                var x = Math.Pow(Math.PI, Math.E);
                var floorX1 = MathUtilities.Floor(x, 10);
                Assert.AreEqual(20, floorX1);
            }
        }

        [TestMethod]
        [TestCategory("MathUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void IntegerFloor()
        {
            var x = 51234;
            {
                var floorX0 = MathUtilities.Floor(x, 1);
                Assert.AreEqual(51234, floorX0);
            }

            {
                var floorX4 = MathUtilities.Floor(x, 10000);
                Assert.AreEqual(50000, floorX4);
            }
        }
    }
}
