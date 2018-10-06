using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class XmlReferenceTest
    {
        private IContext _context;

        private void Given_A_Context()
        {
            if (_context != null)
                return;

            _context = XmlLoader.Load("..\\..\\TestFiles\\Reference-Test.xml");
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
