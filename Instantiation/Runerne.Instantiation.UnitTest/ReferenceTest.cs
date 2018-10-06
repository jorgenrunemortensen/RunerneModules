using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.UnitTest
{
    [TestClass]
    public class ReferenceTest
    {
        [TestMethod]
        [TestCategory("Reference")]
        [TestCategory("GatedCheckIn")]

        public void LateReferenceTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("A", new SimpleInstanceProvider("This is a test!"));
            context.AddNamedInstanceProvider("B", new ReferenceProvider("A", context));
            var s = context.GetInstance<string>("B");
            Assert.AreEqual("This is a test!", s);
        }

        [TestMethod]
        [TestCategory("Reference")]
        [TestCategory("GatedCheckIn")]

        public void EarlyReferenceTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("B", new ReferenceProvider("A", context));
            context.AddNamedInstanceProvider("A", new SimpleInstanceProvider("This is a test!"));
            var s = context.GetInstance<string>("B");
            Assert.AreEqual("This is a test!", s);
        }
    }
}
