using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class XmlLoaderTest
    {
        [TestMethod]
        [TestCategory("XML Loader")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void XDocumentLoadTest()
        {
            var xDocument = XDocument.Load("..\\..\\TestFiles\\List-Test.xml");
            XmlLoader.Parse(xDocument);
        }

        [TestMethod]
        [TestCategory("XML Loader")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void XmlParseTest()
        {
            var xml = File.ReadAllText("..\\..\\TestFiles\\List-Test.xml");
            XmlLoader.Parse(xml);
        }
    }
}
