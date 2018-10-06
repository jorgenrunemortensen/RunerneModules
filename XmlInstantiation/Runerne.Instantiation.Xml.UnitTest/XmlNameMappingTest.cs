using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class XmlNameMappingTest
    {
        private IContext _context;

        private void Given_A_Context()
        {
            if (_context != null)
                return;

            _context = XmlLoader.Load("..\\..\\TestFiles\\Simple-Test.xml");
        }

        [TestMethod]
        [TestCategory("NameMapping")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void NameMappingMaleTest1()
        {
            Given_A_Context();
            var obj = _context.GetInstance("Frank");
            Assert.AreEqual(Gender.Male, obj);
        }

        [TestMethod]
        [TestCategory("NameMapping")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void NameMappingMaleTest2()
        {
            Given_A_Context();
            var obj = _context.GetInstance("Sally");
            Assert.AreEqual(Gender.Female, obj);
        }

        [TestMethod]
        [TestCategory("NameMapping")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void NameMappingMaleTest3()
        {
            Given_A_Context();
            var obj = _context.GetInstance("Wendy");
            Assert.AreEqual(Gender.Female, obj);
        }
    }
}
