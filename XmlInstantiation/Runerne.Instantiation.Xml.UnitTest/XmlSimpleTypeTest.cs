using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    /// <summary>
    /// Summary description for XmlEnumTest
    /// </summary>
    [TestClass]
    public class XmlSimpleTypeTest
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
        [TestCategory("SimpleTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void NothingTest()
        {
            Given_A_Context();
            var obj = _context.GetInstance("Nothing 1");
            Assert.AreEqual("", obj);
        }

        [TestMethod]
        [TestCategory("SimpleTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void ByteTest()
        {
            Given_A_Context();
            var obj = _context.GetInstance("Byte 1");
            Assert.AreEqual((byte)136, obj);
        }

        [TestMethod]
        [TestCategory("SimpleTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void IntTest1()
        {
            Given_A_Context();
            var obj = _context.GetInstance("Int 1");
            Assert.AreEqual((int)2147483640, obj);
        }

        [TestMethod]
        [TestCategory("SimpleTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void IntTest2()
        {
            Given_A_Context();
            var obj = _context.GetInstance("Int 2");
            Assert.AreEqual((int)-2147483620, obj);
        }

        [TestMethod]
        [TestCategory("SimpleTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void DoubleTest1()
        {
            Given_A_Context();
            var obj = _context.GetInstance("Double 1");
            Assert.AreEqual((double)1e100, obj);
        }

        [TestMethod]
        [TestCategory("SimpleTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void DoubleTest2()
        {
            Given_A_Context();
            var obj = _context.GetInstance("Double 2");
            Assert.AreEqual((double)-1.4e101, obj);
        }

        [TestMethod]
        [TestCategory("SimpleTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void BooleanTest()
        {
            Given_A_Context();
            var obj = _context.GetInstance("RightOrWrong");
            Assert.AreEqual(true, obj);
        }

        [TestMethod]
        [TestCategory("SimpleTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void EnumTest()
        {
            Given_A_Context();
            var obj = _context.GetInstance("His gender");
            Assert.AreEqual(Gender.Male, obj);
        }
    }
}
