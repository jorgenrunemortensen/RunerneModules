using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    enum Gender
    {
        Male,
        Female,
    }

    [TestClass]
    public class XmlComplexTypeTest
    {
        private IContext _context;

        private void Given_A_Complex_Context()
        {
            if (_context != null)
                return;

            _context = XmlLoader.Load("..\\..\\TestFiles\\Complex-Test.xml");
        }

        [TestMethod]
        [TestCategory("Xml")]
        [TestCategory("ComplexTypes")]
        [TestCategory("GatedCheckIn")]
        public void ComplexInstancesTest()
        {
            Given_A_Complex_Context();
            var wiName = _context.GetInstance("RI-Name1") as RIName;
            Assert.IsNotNull(wiName, "The loaded instance was of a wrong type.");
            Assert.AreEqual(RINamespace.Default, wiName.Namespace);
            Assert.AreEqual("This is a name", wiName.LocalName);
        }

        [TestMethod]
        [TestCategory("Xml")]
        [TestCategory("ComplexTypes")]
        [TestCategory("GatedCheckIn")]
        public void ComplexInstancesPropertyTest()
        {
            Given_A_Complex_Context();
            var address = _context.GetInstance("Address-1") as Address;
            Assert.AreEqual("Vesterbygdvej", address.StreetName);
            Assert.AreEqual(10, address.HouseNumber);
            Assert.IsNull(address.HouseCode);
            Assert.AreEqual("Ølstykke", address.City);
        }
    }
}