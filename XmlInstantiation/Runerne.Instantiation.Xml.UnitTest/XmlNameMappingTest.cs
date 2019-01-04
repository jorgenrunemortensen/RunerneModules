using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class XmlNameMappingTest
    {
        private static readonly string SimpleXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<context xmlns=""http://runerne.dk/Instantiation.Xml"">
  <includes>
    <include>Runerne.Instantiation.dll</include>
  </includes>
  <instances>
    <simple-instance name=""Nothing 1""/>
    <simple-instance name=""Byte 1"">136</simple-instance >
    <simple-instance name=""Int 1"">2147483640</simple-instance >
    <simple-instance name=""Int 2"">-2147483620</simple-instance >
    <simple-instance name=""Double 1"">1e100</simple-instance>
    <simple-instance name=""Double 2"">-1,4e101</simple-instance>
    <simple-instance name=""Text 1"">This is a string.</simple-instance>
    <simple-instance name=""RightOrWrong"" type=""boolean"">true</simple-instance>
    <simple-instance name=""His gender"" type=""Runerne.Instantiation.Xml.UnitTest.Gender"">Male</simple-instance>
    <simple-instance name=""Her gender"" type=""Runerne.Instantiation.Xml.UnitTest.Gender"">Female</simple-instance>
  </instances>
  <name-mappings>
    <name-mapping to=""His gender"" from=""Frank""/>
    <name-mapping to=""Her gender"">
      <from>Sally</from>
      <from>Wendy</from>
    </name-mapping>
  </name-mappings>
</context>";

        private IContext _context;

        private void Given_A_Context()
        {
            if (_context != null)
                return;

            _context = XmlLoader.Parse(SimpleXml);
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
