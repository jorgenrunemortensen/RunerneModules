using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class XmlNullTest
    {
        private static readonly string NullXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<context xmlns=""http://runerne.dk/Instantiation.Xml"">
  <includes>
    <include>Runerne.Instantiation.dll</include>
    <include>Runerne.Instantiation.Xml.UnitTest.dll</include>
  </includes>
  <instances>
    <null-instance name=""Null string"" type=""string""/>
    <null-instance name=""Typeless""/>
  </instances>
</context>";

        private IContext _context;

        private void Given_A_Context()
        {
            if (_context != null)
                return;

            _context = XmlLoader.Parse(NullXml);
        }

        [TestMethod]
        [TestCategory("NullTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void NullTest()
        {
            Given_A_Context();
            var obj = _context.GetInstance("Null string");
            Assert.IsNull(obj);
        }
    }
}
