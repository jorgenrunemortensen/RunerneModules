using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class XmlStaticTest
    {
        private static readonly string StaticXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<context xmlns=""http://runerne.dk/Instantiation.Xml"">
  <instances>
    <static-instance name=""My static field value"" type=""Runerne.Instantiation.Xml.UnitTest.MyValues"" member=""MyField""/>
    <static-instance name=""My static property value"" type=""Runerne.Instantiation.Xml.UnitTest.MyValues"" member=""MyProperty""/>
    <static-instance name=""My const value"" type=""Runerne.Instantiation.Xml.UnitTest.MyValues"" member=""MyConstant""/>
  </instances>
</context>
";

        private IContext _context;

        private void Given_A_Context()
        {
            if (_context != null)
                return;

            _context = XmlLoader.Parse(StaticXml);
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
