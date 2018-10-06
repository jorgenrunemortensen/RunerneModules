using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.UnitTest
{
    [TestClass]
    public class StaticTest
    {
        [TestMethod]
        [TestCategory("Static")]
        [TestCategory("GatedCheckIn")]
        public void StaticFieldStringTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("A", new StaticInstanceProvider(typeof(Runerne.Instantiation.UnitTest.StaticClass), "Field"));
            var s = context.GetInstance<string>("A");
            Assert.IsInstanceOfType(s, typeof(string));
            Assert.AreEqual("The content of the field", s);
        }

        [TestMethod]
        [TestCategory("Static")]
        [TestCategory("GatedCheckIn")]
        public void StaticPropertyStringTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("A", new StaticInstanceProvider(typeof(Runerne.Instantiation.UnitTest.StaticClass), "Property"));
            var s = context.GetInstance<string>("A");
            Assert.IsInstanceOfType(s, typeof(string));
            Assert.AreEqual("The content of the property.", s);
        }

        [TestMethod]
        [TestCategory("Static")]
        [TestCategory("GatedCheckIn")]
        public void ConstantStringTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("A", new StaticInstanceProvider(typeof(Runerne.Instantiation.UnitTest.StaticClass), "Constant"));
            var s = context.GetInstance<string>("A");
            Assert.IsInstanceOfType(s, typeof(string));
            Assert.AreEqual("The content of the constant.", s);
        }
    }
}
