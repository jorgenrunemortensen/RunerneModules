using Microsoft.VisualStudio.TestTools.UnitTesting;
using Runerne.Computation.Numeric;
using Runerne.Computation.String;

namespace Runerne.Computation.UnitTest
{
    [TestClass]
    public class StringTest
    {
        [TestMethod]
        [TestCategory("String")]
        [TestCategory("GatedCheckIn")]
        public void ConcatenateStringsTest()
        {
            var sut = new ConcatenateStrings(
                new StringConstant("This"),
                new StringConstant(" "),
                new StringConstant("is"),
                new StringConstant(" "),
                new StringConstant("great")
            );
            Assert.AreEqual("This is great", sut.Value);
            //Assert.Fail();
        }

        [TestMethod]
        [TestCategory("String")]
        [TestCategory("GatedCheckIn")]
        public void IndexOfTest()
        {
            {
                var sut = new IndexOfString(
                    new StringConstant("This is a test"),
                    new StringConstant("is")
                    );
                Assert.AreEqual(2, sut.Value);
            }

            {
                var sut = new IndexOfString(
                    new StringConstant("This is a test"),
                    new StringConstant("is"),
                    new NumericConstant(3)
                    );
                Assert.AreEqual(5, sut.Value);
            }
        }

        [TestMethod]
        [TestCategory("String")]
        [TestCategory("GatedCheckIn")]
        public void StringLengthTest()
        {
            var sut = new StringLength(new StringConstant("Hello world!"));
            Assert.AreEqual(12, sut.Value);
        }

        [TestMethod]
        [TestCategory("String")]
        [TestCategory("GatedCheckIn")]
        public void SubStringTest()
        {
            var sut = new SubString(
                new StringConstant("Hello World!"),
                new NumericConstant(6),
                new NumericConstant(5)
            );
            Assert.AreEqual("World", sut.Value);
        }

        [TestMethod]
        [TestCategory("String")]
        [TestCategory("GatedCheckIn")]
        public void ToLowerInvariantTest()
        {
            var sut = new ToLowerInvariant(new StringConstant("This is KMD"));
            Assert.AreEqual("this is kmd", sut.Value);
        }

        [TestMethod]
        [TestCategory("String")]
        [TestCategory("GatedCheckIn")]
        public void ToUpperInvariantTest()
        {
            var sut = new ToUpperInvariant(new StringConstant("This is KMD"));
            Assert.AreEqual("THIS IS KMD", sut.Value);
        }

        [TestMethod]
        [TestCategory("String")]
        [TestCategory("GatedCheckIn")]
        public void StringEqualsTest()
        {
            {
                var sut = new StringEquals(
                    new StringConstant("HELLO"),
                    new ToUpperInvariant(new StringConstant("Hello"))
                 );
                Assert.IsTrue(sut.Value);
            }

            {
                var sut = new StringEquals(
                    new StringConstant("HELLO"),
                    new ToUpperInvariant(new StringConstant("Hello")),
                    new StringConstant("HELLO")
                 );
                Assert.IsTrue(sut.Value);
            }

            {
                var sut = new StringEquals(
                    new StringConstant("HELLO"),
                    new StringConstant("Hello"),
                    new StringConstant("HELLO")
                 );
                Assert.IsFalse(sut.Value);
            }

            {
                var sut = new StringEquals(
                    new StringConstant("HELLO"),
                    new StringConstant(null)
                );
                Assert.IsFalse(sut.Value);
            }

            {
                var sut = new StringEquals(
                    new StringConstant(null),
                    new StringConstant("HELLO")
                );
                Assert.IsFalse(sut.Value);
            }
        }

        [TestMethod]
        [TestCategory("String")]
        [TestCategory("GatedCheckIn")]
        public void StringVariableInitialValueTest()
        {
            var variable = new StringVariable("The initial text");
            Assert.AreEqual("The initial text", variable.Value);
        }

        [TestMethod]
        [TestCategory("String")]
        [TestCategory("GatedCheckIn")]
        public void StringVariableChangeValueTest()
        {
            var variable = new StringVariable("My initial text");
            Assert.AreEqual("My initial text", variable.Value);
            variable.Value = "Hello world!";
            Assert.AreEqual("Hello world!", variable.Value);
        }
    }
}
