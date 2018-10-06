using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.UnitTest
{
    [TestClass]
    public class SimpleTypeTest
    {
        [TestMethod]
        [TestCategory("Simple Type")]
        [TestCategory("GatedCheckIn")]

        public void DoubleTest()
        {
            var mySimpleProvider = new SimpleInstanceProvider(3.14);
            var instance = mySimpleProvider.GetInstance();
            Assert.IsInstanceOfType(instance, typeof(double));
            Assert.AreEqual(3.14, instance);
        }
    }
}
