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


        private static readonly string ComplexXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<context xmlns=""http://runerne.dk/Instantiation.Xml"">
  <includes>
    <include>Runerne.Instantiation.dll</include>
    <include>Runerne.Instantiation.Xml.UnitTest.dll</include>
  </includes>
  <instances>
    <simple-instance name=""Nothing 1""/>
    <simple-instance name=""Byte 1"">36</simple-instance >
    <simple-instance name=""Int 1"">256</simple-instance >
    <simple-instance name=""Int 2"">-15</simple-instance >
    <simple-instance name=""Double 1"">,14</simple-instance>
    <simple-instance name=""Double 2"">-,14</simple-instance>
    <simple-instance name=""Double 3"">3,14</simple-instance>
    <simple-instance name=""Double 4"">-3,14</simple-instance>
    <simple-instance name=""Double 5"">3,14e-05</simple-instance>
    <simple-instance name=""Double 6"">-3,14e-05</simple-instance>
    <simple-instance name=""City Name"">Ølstykke</simple-instance>
    
    <complex-instance name=""RI-Name1"" class=""Runerne.Instantiation.RIName"">
      <constructor-args>
        <simple-instance>This is a name</simple-instance>
      </constructor-args>
    </complex-instance>
    
    <complex-instance name=""Address-1"" class=""Runerne.Instantiation.Xml.UnitTest.Address"">
      <properties>
        <simple-instance name=""StreetName"">Vesterbygdvej</simple-instance>
        <simple-instance name=""HouseNumber"">10</simple-instance>
        <reference name=""City"" ref=""City Name""/>
      </properties>
    </complex-instance>
  </instances>

</context>";

        private IContext _context;

        private void Given_A_Complex_Context()
        {
            if (_context != null)
                return;

            _context = XmlLoader.Parse(ComplexXml);
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