using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using Runerne.Computation.Numeric;

namespace Runerne.Computation.UnitTest
{
    [TestClass]
    public class NumericTest
    {
        [TestMethod]
        [TestCategory("Numeric")]
        [TestCategory("GatedCheckIn")]
        public void TestAdd()
        {
            var adder = new Add(
                new NumericConstant(3),
                new NumericConstant(5.5)
            );
            Assert.AreEqual(8.5, adder.Value);
        }

        [TestMethod]
        [TestCategory("Numeric")]
        [TestCategory("GatedCheckIn")]
        public void TestAdd1()
        {
            var inputs = new[]
            {
                new NumericConstant(3),
                new NumericConstant(5.5)
            };

            var adder = new Add(inputs);
            Assert.AreEqual(8.5, adder.Value);
        }

        [TestMethod]
        [TestCategory("Numeric")]
        [TestCategory("GatedCheckIn")]
        public void TestMultiply()
        {
            var multiplier = new Multiply(
                new NumericConstant(3),
                new NumericConstant(5.5)
            );
            Assert.AreEqual(16.5, multiplier.Value);
        }

        [TestMethod]
        [TestCategory("Numeric")]
        [TestCategory("GatedCheckIn")]
        public void TestInvert()
        {
            var inverter = new Invert(new NumericConstant((4)));
            Assert.AreEqual(0.25, inverter.Value);
        }

        [TestMethod]
        [TestCategory("Numeric")]
        [TestCategory("GatedCheckIn")]
        public void TestNegate()
        {
            var negater = new Negate(new NumericConstant(36));
            Assert.AreEqual(-36, negater.Value);
        }

        [TestMethod]
        [TestCategory("Numeric")]
        [TestCategory("GatedCheckIn")]
        public void NumericCombibationTest()
        {
            var adder = new Add(
                new Multiply(
                    new NumericConstant(3),
                    new Negate(new NumericConstant(5))
                ),
                new Invert(new NumericConstant(4))
            );
            Assert.AreEqual(-14.75, adder.Value);
        }

        [TestMethod]
        [TestCategory("Numeric")]
        [TestCategory("GatedCheckIn")]
        public void NumericVariableInitialValueTest()
        {
            var variable = new NumericVariable(3.14);
            Assert.AreEqual(3.14, variable.Value);
        }

        [TestMethod]
        [TestCategory("Numeric")]
        [TestCategory("GatedCheckIn")]
        public void NumericVariableChangeValueTest()
        {
            var variable = new NumericVariable(3.14);
            Assert.AreEqual(3.14, variable.Value);
            variable.Value = -36;
            Assert.AreEqual(-36, variable.Value);
        }

        [TestMethod]
        [TestCategory("Numeric")]
        [TestCategory("GatedCheckIn")]
        public void EnumToNumericTest()
        {
            {
                var femaleComputable = new ToNumeric(Gender.Female);
                Assert.AreEqual(0, femaleComputable.Value);
            }

            {
                var maleComputable = new ToNumeric(Gender.Male);
                Assert.AreEqual(1, maleComputable.Value);
            }
        }

        [TestMethod]
        [TestCategory("Numeric")]
        [TestCategory("GatedCheckIn")]
        public void StringToNumericTest()
        {
            var computable = new ToNumeric($"3{NumberFormatInfo.CurrentInfo.NumberDecimalSeparator}14");
            Assert.AreEqual(3.14, computable.Value);
        }
    }
}
