using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class XmlListTypeTest
    {
        private IContext _context;

        private void Given_A_Context()
        {
            if (_context != null)
                return;

            _context = XmlLoader.Load("..\\..\\TestFiles\\List-Test.xml");
        }

        [TestMethod]
        [TestCategory("Xml")]
        [TestCategory("ListTypes")]
        [TestCategory("GatedCheckIn")]
        public void ListTest1()
        {
            Given_A_Context();
            var list = _context.GetInstance("List 1") as IEnumerable<object>;
            Assert.IsNotNull(list);
            var a = list.ToArray<object>();
            Assert.AreEqual(5, a.Length);
            Assert.AreEqual("Cow", a[0]);
            Assert.AreEqual("Fox", a[1]);
            Assert.AreEqual((float)3.14, a[2]);
            Assert.AreEqual("Cat", a[3]);

            Assert.IsInstanceOfType(a[4], typeof(IList));
            var subList = a[4] as IList;
            var subArray = new object[subList.Count];
            subList.CopyTo(subArray, 0);

            Assert.AreEqual((sbyte)0, subArray[0]);
            Assert.AreEqual((sbyte)1, subArray[1]);
            Assert.AreEqual((sbyte)2, subArray[2]);
        }

        [TestMethod]
        [TestCategory("Xml")]
        [TestCategory("ListTypes")]
        [TestCategory("GatedCheckIn")]
        public void ListTest2()
        {
            Given_A_Context();
            var list = _context.GetInstance("List 2") as IEnumerable<object>;
            var listType = list.GetType();
            Assert.AreEqual(typeof(List<string>), listType);
            Assert.IsNotNull(list);
            var a = list.ToArray<object>();
            Assert.AreEqual(2, a.Length);

            Assert.IsInstanceOfType(a[0], typeof(string));
            Assert.AreEqual("0", a[0]);

            Assert.IsInstanceOfType(a[1], typeof(string));
            Assert.AreEqual("3,14", a[1]);
        }

        [TestMethod]
        [TestCategory("Xml")]
        [TestCategory("ListTypes")]
        [TestCategory("GatedCheckIn")]
        public void ListTest3()
        {
            Given_A_Context();
            var list = _context.GetInstance("List 3") as IEnumerable<double>;
            var listType = list.GetType();
            Assert.AreEqual(typeof(List<double>), listType);
            Assert.IsNotNull(list);
            var a = list.ToArray<double>();
            Assert.AreEqual(2, a.Length);

            Assert.AreEqual((double)0, a[0]);
            Assert.AreEqual((double)3.14, a[1], 0.0001);
        }
    }
}