using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class XmlStaticTest
    {
        private IContext _context;

        private void Given_A_Context()
        {
            if (_context != null)
                return;

            _context = XmlLoader.Load("..\\..\\TestFiles\\Static-Test.xml");
        }

        [TestMethod]
        [TestCategory("StaticTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void FieldTest()
        {
            Given_A_Context();
            var obj = _context.GetInstance("My static field value");
            Assert.IsInstanceOfType(obj, typeof(string));
            Assert.AreEqual("The value of the static field", obj);
        }

        [TestMethod]
        [TestCategory("StaticTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void PropertyTest()
        {
            Given_A_Context();
            var obj = _context.GetInstance("My static property value");
            Assert.IsInstanceOfType(obj, typeof(string));
            Assert.AreEqual("The value of the static property", obj);
        }

        [TestMethod]
        [TestCategory("StaticTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void ConstantTest()
        {
            Given_A_Context();
            var obj = _context.GetInstance("My const value");
            Assert.IsInstanceOfType(obj, typeof(string));
            Assert.AreEqual("The value of the constant", obj);
        }
    }
}
