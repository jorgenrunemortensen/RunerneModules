using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class XmlNullTest
    {
        private IContext _context;

        private void Given_A_Context()
        {
            if (_context != null)
                return;

            _context = XmlLoader.Load("..\\..\\TestFiles\\Null-Test.xml");
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
