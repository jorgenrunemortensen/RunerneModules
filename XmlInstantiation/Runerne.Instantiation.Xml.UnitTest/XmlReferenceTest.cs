using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class XmlReferenceTest
    {
        private static readonly string ReferenceXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<context xmlns=""http://runerne.dk/Instantiation.Xml"">
  <includes>
    <include>Runerne.Instantiation.dll</include>
    <include>Runerne.Instantiation.Xml.UnitTest.dll</include>
  </includes>
  <instances>
    <reference name=""Lazy evaluated rerence"" ref=""My object 1""/>
    <simple-instance name=""My object 1"" type=""string"">This is my object</simple-instance>
    <reference name=""My indirect object 1"" ref=""My object 1""/>
  </instances>
</context>";

        private IContext _context;

        private void Given_A_Context()
        {
            if (_context != null)
                return;

            _context = XmlLoader.Parse(ReferenceXml);
        }


        [TestMethod]
        [TestCategory("Reference")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void NullTest()
        {
            Given_A_Context();
            var mainObj = _context.GetInstance("My object 1");
            var indirectObj = _context.GetInstance("My indirect object 1");
            Assert.AreEqual(mainObj, indirectObj);
        }
    }
}
