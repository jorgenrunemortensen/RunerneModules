using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class ArgumentsTest
    {
        private static readonly string ArgumentsXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<context xmlns=""http://runerne.dk/Instantiation.Xml"">
  <includes>
    <include>Runerne.Instantiation.dll</include>
    <include>Runerne.Instantiation.Xml.UnitTest.dll</include>
  </includes>
  <instances>
    <complex-instance name=""Address-1"" class=""Runerne.Instantiation.Xml.UnitTest.Address"">
      <properties>
        <simple-instance name=""StreetName"">Vesterbygdvej</simple-instance>
        <simple-instance name=""HouseNumber"">10</simple-instance>
        <reference name=""City"" ref=""City Name""/>
      </properties>
    </complex-instance>
  </instances>

</context>";
        [TestMethod]
        [TestCategory("Arguments")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void ArgumentTest()
        {
            var context = XmlLoader.Parse(ArgumentsXml,
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
