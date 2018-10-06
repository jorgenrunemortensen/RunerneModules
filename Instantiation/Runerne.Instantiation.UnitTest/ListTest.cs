using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Runerne.Instantiation.UnitTest
{
    [TestClass]
    public class ListTest
    {
        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void ElementTypeDerivingListTest()
        {
            var context = new RIContext();

            var instanceProvider = new ListInstanceProvider(
                new IInstanceProvider[] {
                    new SimpleInstanceProvider("Test"),
                    new SimpleInstanceProvider("Horse"),
                    new SimpleInstanceProvider("Cat"),
                }
            );
            context.AddNamedInstanceProvider("A", instanceProvider);

            var s = context.GetInstance<IEnumerable<string>>("A");
            var a = s.ToArray();
            Assert.AreEqual(3, a.Length);
            Assert.AreEqual("Test", a[0]);
            Assert.AreEqual("Horse", a[1]);
            Assert.AreEqual("Cat", a[2]);
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]


        public void ElementTypeSpecificListTest()
        {
            var context = new RIContext();

            var instanceProvider = new ListInstanceProvider(
                 new IInstanceProvider[] {
                    new SimpleInstanceProvider("Test"),
                    new SimpleInstanceProvider("Horse"),
                    new SimpleInstanceProvider("Cat"),
                },
                typeof(string)
            );
            context.AddNamedInstanceProvider("A", instanceProvider);

            var s = context.GetInstance<IEnumerable<string>>("A");
            var a = s.ToArray();
            Assert.AreEqual(3, a.Length);
            Assert.AreEqual("Test", a[0]);
            Assert.AreEqual("Horse", a[1]);
            Assert.AreEqual("Cat", a[2]);
        }
    }
}
