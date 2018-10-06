using Microsoft.VisualStudio.TestTools.UnitTesting;
using Runerne.Computation.Logic;
using Runerne.Computation.Numeric;

namespace Runerne.Computation.UnitTest
{
    [TestClass]
    public class LogicTest
    {
        [TestMethod]
        [TestCategory("Logic")]
        [TestCategory("GatedCheckIn")]
        public void TestTrue()
        {
            var boolean = BooleanTrue.Instance;
            Assert.AreEqual(true, boolean.Value);
        }

        [TestMethod]
        [TestCategory("Logic")]
        [TestCategory("GatedCheckIn")]
        public void TestFalse()
        {
            var boolean = BooleanFalse.Instance;
            Assert.AreEqual(false, boolean.Value);
        }

        [TestMethod]
        [TestCategory("Logic")]
        [TestCategory("GatedCheckIn")]
        public void TestNot()
        {
            {
                var boolean = new Not(BooleanTrue.Instance);
                Assert.AreEqual(false, boolean.Value);
            }
            {
                var boolean = new Not(BooleanFalse.Instance);
                Assert.AreEqual(true, boolean.Value);
            }
        }

        [TestMethod]
        [TestCategory("Logic")]
        [TestCategory("GatedCheckIn")]
        public void TestAnd()
        {
            {
                var res = new And(
                    BooleanFalse.Instance,
                    BooleanFalse.Instance
                    );
                Assert.AreEqual(false, res.Value);
            }
            {
                var res = new And(
                    BooleanTrue.Instance,
                    BooleanFalse.Instance
                    );
                Assert.AreEqual(false, res.Value);
            }
            {
                var res = new And(
                    BooleanFalse.Instance,
                    BooleanTrue.Instance
                    );
                Assert.AreEqual(false, res.Value);
            }
            {
                var res = new And(
                    BooleanTrue.Instance,
                    BooleanTrue.Instance
                    );
                Assert.AreEqual(true, res.Value);
            }
        }

        [TestMethod]
        [TestCategory("Logic")]
        [TestCategory("GatedCheckIn")]
        public void TestOr()
        {
            {
                var res = new Or(
                    BooleanFalse.Instance,
                    BooleanFalse.Instance
                    );
                Assert.AreEqual(false, res.Value);
            }
            {
                var res = new Or(
                    BooleanTrue.Instance,
                    BooleanFalse.Instance
                    );
                Assert.AreEqual(true, res.Value);
            }
            {
                var res = new Or(
                    BooleanFalse.Instance,
                    BooleanTrue.Instance
                    );
                Assert.AreEqual(true, res.Value);
            }
            {
                var res = new Or(
                    BooleanTrue.Instance,
                    BooleanTrue.Instance
                    );
                Assert.AreEqual(true, res.Value);
            }
        }

        [TestMethod]
        [TestCategory("Logic")]
        [TestCategory("GatedCheckIn")]
        public void TestXOr()
        {
            {
                var res = new XOr(
                    BooleanFalse.Instance,
                    BooleanFalse.Instance
                    );
                Assert.AreEqual(false, res.Value);
            }
            {
                var res = new XOr(
                    BooleanTrue.Instance,
                    BooleanFalse.Instance
                    );
                Assert.AreEqual(true, res.Value);
            }
            {
                var res = new Or(
                    BooleanFalse.Instance,
                    BooleanTrue.Instance
                    );
                Assert.AreEqual(true, res.Value);
            }
            {
                var res = new XOr(
                    BooleanTrue.Instance,
                    BooleanTrue.Instance
                    );
                Assert.AreEqual(false, res.Value);
            }
        }

        [TestMethod]
        [TestCategory("Logic")]
        [TestCategory("GatedCheckIn")]
        public void TestIf()
        {
            {
                var comp = new If<double>(
                    BooleanFalse.Instance,
                    new NumericConstant(3),
                    new NumericConstant(4)
                    );
                Assert.AreEqual(4, comp.Value);
            }
            {
                var comp = new If<double>(
                    BooleanTrue.Instance, 
                    new NumericConstant(3),
                    new NumericConstant(4)
                    );
                Assert.AreEqual(3, comp.Value);
            }
        }

        [TestMethod]
        [TestCategory("Logic")]
        [TestCategory("GatedCheckIn")]
        public void BooleanVariableDefaultValueTest()
        {
            var variable = new BooleanVariable();
            Assert.IsFalse(variable.Value);
        }

        [TestMethod]
        [TestCategory("Logic")]
        [TestCategory("GatedCheckIn")]
        public void BooleanVariableInitialTrueValueTest()
        {
            var variable = new BooleanVariable(true);
            Assert.IsTrue(variable.Value);
        }

        [TestMethod]
        [TestCategory("Logic")]
        [TestCategory("GatedCheckIn")]
        public void BooleanVariableInitialFalseValueTest()
        {
            var variable = new BooleanVariable(false);
            Assert.IsFalse(variable.Value);
        }

        [TestMethod]
        [TestCategory("Logic")]
        [TestCategory("GatedCheckIn")]
        public void BooleanVariableChangeValueTest()
        {
            var variable = new BooleanVariable(false);
            Assert.IsFalse(variable.Value);
            variable.Value = true;
            Assert.IsTrue(variable.Value);
        }
    }
}

