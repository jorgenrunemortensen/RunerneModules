using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class ArgumentsTest
    {
        [TestMethod]
        [TestCategory("Arguments")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void ArgumentTest()
        {
            var context = XmlLoader.Load("..\\..\\TestFiles\\Arguments-Test.xml",
                new Dictionary<string, object> {
                    { "City Name", "Ølstykke" }
                }
            );  

            var address = context.GetInstance<Address>("Address-1");
            Assert.IsNotNull(address);
            Assert.AreEqual("Vesterbygdvej", address.StreetName);
            Assert.AreEqual("Ølstykke", address.City);
        }
    }
}
