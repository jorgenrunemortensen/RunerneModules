using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.UnitTest
{
    [TestClass]
    public class NamespaceTest
    {
        [TestMethod]
        [TestCategory("Namespace")]
        [TestCategory("GatedCheckIn")]

        public void ValidNamespaceToStringTest()
        {
            RINamespace ns = "The namespace";
            Assert.AreEqual("{The namespace}", ns.ToString());
        }

        [TestMethod]
        [TestCategory("Namespace")]
        [TestCategory("GatedCheckIn")]

        public void InvalidNamespaceTest1()
        {
            try
            {
                RINamespace ns = "{Name";
                Assert.Fail("The namespace should not accept the '{' character.");
            }
            catch (RINamespaceException)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("Namespace")]
        [TestCategory("GatedCheckIn")]

        public void InvalidNamespaceTest2()
        {
            try
            {
                RINamespace ns = "Name}";
                Assert.Fail("The namespace should not accept the '}' character.");
            }
            catch (RINamespaceException)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("Namespace")]
        [TestCategory("GatedCheckIn")]

        public void InvalidNamespaceTest3()
        {
            try
            {
                RINamespace ns = "Na{me";
                Assert.Fail("The namespace should not accept the '{' character.");
            }
            catch (RINamespaceException)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("Namespace")]
        [TestCategory("GatedCheckIn")]

        public void InvalidNamespaceTest4()
        {
            try
            {
                RINamespace ns = "Na}me";
                Assert.Fail("The namespace should not accept the '}' character.");
            }
            catch (RINamespaceException)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("Namespace")]
        [TestCategory("GatedCheckIn")]

        public void InvalidNamespaceTest5()
        {
            try
            {
                RINamespace ns = "N{a}me";
                Assert.Fail("The namespace should not accept the '}' or the '}' character.");
            }
            catch (RINamespaceException)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("Namespace")]
        [TestCategory("GatedCheckIn")]

        public void DefaultNamespaceTest()
        {
            RIName name1 = "Instance 1";
            Assert.AreEqual("Instance 1", name1.ToString());
        }

        [TestMethod]
        [TestCategory("Namespace")]
        [TestCategory("GatedCheckIn")]

        public void CompareEqualNamespacesTest()
        {
            RINamespace ns1 = "ns1";
            RINamespace ns2 = "ns1";

            Assert.IsTrue(ns1 == ns2);
            Assert.AreEqual(ns1, ns2);
        }

        [TestMethod]
        [TestCategory("Namespace")]
        [TestCategory("GatedCheckIn")]

        public void CompareUnequalNamespaceTest()
        {
            RINamespace ns1 = "ns1";
            RINamespace ns2 = "ns2";

            Assert.IsTrue(ns1 != ns2);
            Assert.AreNotEqual(ns1, ns2);
        }

        [TestMethod]
        [TestCategory("Namespace")]
        [TestCategory("GatedCheckIn")]

        public void CompareEqualDefaultNamespacesTest()
        {
            RINamespace ns1 = "";
            RINamespace ns2 = "";

            Assert.IsTrue(ns1 == ns2);
            Assert.AreEqual(ns1, ns2);
        }

        [TestMethod]
        [TestCategory("Namespace")]
        [TestCategory("GatedCheckIn")]

        public void CompareUnequalDefaultNamespacedTest1()
        {
            RINamespace ns1 = "";
            RINamespace ns2 = "ns2";

            Assert.IsTrue(ns1 != ns2);
            Assert.AreNotEqual(ns1, ns2);
        }

        [TestMethod]
        [TestCategory("Namespace")]
        [TestCategory("GatedCheckIn")]

        public void CompareUnequalDefaultNamespacedTest2()
        {
            RINamespace ns1 = "ns1";
            RINamespace ns2 = "";

            Assert.IsTrue(ns1 != ns2);
            Assert.AreNotEqual(ns1, ns2);
        }

    }
}
