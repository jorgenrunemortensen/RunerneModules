using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    /// <summary>
    /// Summary description for XmlEnumTest
    /// </summary>
    [TestClass]
    public class XmlSimpleTypeTest
    {
        private IContext _context;

        private void Given_A_Context()
        {
            if (_context != null)
                return;

            _context = XmlLoader.Load("..\\..\\TestFiles\\Simple-Test.xml");
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
