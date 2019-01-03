using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Xml.UnitTest
{
    [TestClass]
    public class RemoveAllNamespacesTest
    {
        private static readonly string Input = @"<?xml version=""1.0"" encoding=""utf-8""?>
<!DOCTYPE chapter SYSTEM ""../dtds/chapter.dtd"">
<Fejl xmlns=""urn:oio:dkal:1.0.0"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:jgm=""xyz"">
    <?PITarget PIContent?>
    <FejlIdentifikator>2017-12-05-15.11.57.482962</FejlIdentifikator>
    <jgm:FejlKode>3000</jgm:FejlKode>
    <FejlTekst>Ingen adgang
Klientcertifikatet er ikke gyldigt</FejlTekst>
    <!-- This is a comment -->
    <![CDATA[Dette er en hest]]>
</Fejl>";
        private static readonly string ExpectedOutput = @"<!DOCTYPE chapter SYSTEM ""../dtds/chapter.dtd""[]>
<Fejl>
  <?PITarget PIContent?>
  <FejlIdentifikator>2017-12-05-15.11.57.482962</FejlIdentifikator>
  <FejlKode>3000</FejlKode>
  <FejlTekst>Ingen adgang
Klientcertifikatet er ikke gyldigt</FejlTekst>
  <!-- This is a comment --><![CDATA[Dette er en hest]]></Fejl>";

        [TestMethod]
        [TestCategory("Xml Utilities")]
        [TestCategory("GatedCheckIn")]
        public void RemoveAllNamespaces_Test()
        {
            var inputXDocument = XDocument.Parse(Input);
            var outputDocument = XmlUtilities.RemoveAllNamespaces(inputXDocument);

            Assert.AreEqual(ExpectedOutput, outputDocument.ToString());
        }
    }
}
