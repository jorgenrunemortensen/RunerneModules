using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Xml.UnitTest
{
    [TestClass]
    public class RemoveAllNamespacesTest
    {
        [TestMethod]
        [TestCategory("Xml Utilities")]
        [TestCategory("GatedCheckIn")]
        public void RemoveAllNamespaces_Test()
        {
            var inputXDocument = XDocument.Load("Stimulus-1.xml");
            var outputDocument = XmlUtilities.RemoveAllNamespaces(inputXDocument);

            var expectedOutput = File.ReadAllText("Expected-1.xml");
            Assert.AreEqual(expectedOutput, outputDocument.ToString());
        }
    }
}
