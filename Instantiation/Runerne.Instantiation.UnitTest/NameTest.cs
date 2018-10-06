using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.UnitTest
{
    [TestClass]
    public class NameTest
    {
        [TestMethod]
        [TestCategory("RIName")]
        [TestCategory("GatedCheckIn")]

        public void CombinedToStringTest()
        {
            RINamespace myFirstNamespace = "My 1st namespace";
            var mySecondNamespace = new RINamespace("My 2nd namespace");
            var name1 = new RIName(myFirstNamespace, "Instance 1");

            Assert.AreEqual("{My 1st namespace}Instance 1", name1.ToString());
        }

        [TestMethod]
        [TestCategory("RIName")]
        [TestCategory("GatedCheckIn")]

        public void InvalidNameTest1()
        {
            try
            {
                RIName name = "{name";
                Assert.Fail("The name should not accept the '{' character.");
            }
            catch (RINameException)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("RIName")]
        [TestCategory("GatedCheckIn")]

        public void InvalidNameTest2()
        {
            try
            {
                RIName name = "name}";
                Assert.Fail("The name should not accept the '}' character.");
            }
            catch (RINameException)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("RIName")]
        [TestCategory("GatedCheckIn")]

        public void InvalidNameTest3()
        {
            try
            {
                RIName name = "nam}e";
                Assert.Fail("The name should not accept the '}' character.");
            }
            catch (RINameException)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("RIName")]
        [TestCategory("GatedCheckIn")]

        public void InvalidNameTest4()
        {
            try
            {
                RIName name = "na{me";
                Assert.Fail("The name should not accept the '{' character.");
            }
            catch (RINameException)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("RIName")]
        [TestCategory("GatedCheckIn")]

        public void InvalidNameTest5()
        {
            try
            {
                RIName name = "na{m}e";
                Assert.Fail("The name should not accept the '{' or the '}' character.");
            }
            catch (RINameException)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("RIName")]
        [TestCategory("GatedCheckIn")]

        public void CompareEqualNames()
        {
            RIName name1 = "{ns1}abe";
            RIName name2 = "{ns1}abe";

            Assert.IsTrue(name1 == name2);
            Assert.AreEqual(name1, name2);
        }

        [TestMethod]
        [TestCategory("RIName")]
        [TestCategory("GatedCheckIn")]

        public void CompareUnequalNames()
        {
            RIName name1 = "{ns1}abe";
            RIName name2 = "{ns1}monkey";

            Assert.IsTrue(name1 != name2);
            Assert.AreNotEqual(name1, name2);
        }

        [TestMethod]
        [TestCategory("RIName")]
        [TestCategory("GatedCheckIn")]

        public void NamespacedNameTest()
        {
            RIName name = "{TheNamespaceName}TheLocalName";
            Assert.AreEqual("TheLocalName", name.LocalName);
            Assert.AreEqual("{TheNamespaceName}", name.Namespace.ToString());
            Assert.AreEqual("{TheNamespaceName}TheLocalName", name);
        }
    }
}
