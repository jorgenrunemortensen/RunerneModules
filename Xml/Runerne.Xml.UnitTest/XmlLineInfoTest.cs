using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace Runerne.Xml.UnitTest
{
    [TestClass]
    public class XmlLineInfoTest
    {
        private XDocument _document;

        [TestMethod]
        [TestCategory("Xml Utilities")]
        [TestCategory("GatedCheckInDisabled")]
        public void LineNumber1_Test()
        {
            Given_A_LineNumberedDocument();
            var element = _document.Element("root").Element("e-1").Element("e-1-2");
            var lineInfoText = XmlUtilities.GetLineInfoText(element);
            Assert.AreEqual("Line = 4, Column = 6", lineInfoText);

        }

        [TestMethod]
        [TestCategory("Xml Utilities")]
        [TestCategory("GatedCheckInDiabled")]
        public void LineNumber2_Test()
        {
            Given_A_NonLineNumberedDocument();
            var element = _document.Element("root").Element("e-1").Element("e-1-2");
            var lineInfoText = XmlUtilities.GetLineInfoText(element);
            Assert.AreEqual(XmlUtilities.NoLineNumberDefinedText, lineInfoText);
        }

        private static readonly string Xml =
@"<root>
  <e-1>
    <e-1-1/>
    <e-1-2/>
  </e-1>
  <e-2/>
</root>";

        private void Given_A_LineNumberedDocument()
        {
            _document = XDocument.Parse(Xml, LoadOptions.SetLineInfo);
        }

        private void Given_A_NonLineNumberedDocument()
        {
            _document = XDocument.Parse(Xml);
        }
    }
}
