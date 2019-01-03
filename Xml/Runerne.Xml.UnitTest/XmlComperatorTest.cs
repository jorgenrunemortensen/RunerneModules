using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Xml.UnitTest
{
    [TestClass]
    public class XmlComperatorTest
    {
        private static readonly string PersonXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<root>
  <addresses>
    <address id=""1"">
      <address-line>Vesterbygdvej 10</address-line>
      <postal-code>3650</postal-code>
      <city>Ølstykke</city>
      <country>DK</country>
    </address>  
  </addresses>
  
  <persons>
    <person>
      <first-name>Jørgen</first-name>
      <last-name>Mortensen</last-name>
      <address-ref id=""1""/>
    </person>
    
    <person>
      <first-name>Bente</first-name>
      <last-name>Mortensen</last-name>
      <address-ref id=""1""/>
    </person>
  </persons>
</root>
";

        private static readonly string Mozart_Utf8 = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<composers>
  <composer>
    <name>Wolfgang Amadeus Mozart</name>
    <type>classical</type>
  </composer>
</composers>
";

        private static readonly string Mozart_8859_1 = @"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>
<composers>
  <composer>
    <name>Wolfgang Amadeus Mozart</name>
    <type>classical</type>
  </composer>
</composers>
";

        private static readonly string Mozart_8859_1_CData = @"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>
<composers>
  <composer>
    <name>Wolfgang Amadeus Mozart</name>
    <type><![CDATA[classical]]></type>
  </composer>
</composers>
";

        private static readonly string Mozard_8859_1_CData_Combined = @"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>
<composers>
  <composer>
    <name>Wolfgang Amadeus Mozart</name>
    <type>cl<![CDATA[ass]]><![CDATA[ic]]>a<![CDATA[l]]></type>
  </composer>
</composers>
";

        private static readonly string Detail_ABCD = @"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>
<master>
  <detail a=""1"" b=""2"" c=""3"" d=""4""/>
</master>";

        private static readonly string Detail_ACD = @"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>
<master>
  <detail a=""1"" c=""3"" d=""4""/>
</master>";

        private static readonly string Detail_EFG = @"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>
<master>
  <detail e=""5"" f=""6"" g=""7""/>
</master>";

        private static readonly string Detail_BD = @"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>
<master>
  <detail b=""2"" d=""4""/>
</master>";

        private static readonly string Detail_BED = @"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>
<master>
  <detail b=""2"" e=""5"" d=""4""/>
</master>";

        private static readonly string Detail_ACDB = @"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>
<master>
  <detail a=""1"" c=""3"" d=""4"" b=""2""/>
</master>";

        private static readonly string Comment1 = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<level-0>
  <level-1>
    <!-- And we repeat this until level 42 -->
    <level-42/>
  </level-1>
</level-0>";

        private static readonly string Comment2 = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<level-0>
  <level-1>
    <!-- Do not repeat this until level 42 -->
    <level-42/>
  </level-1>
</level-0>";

        private static readonly string ProcessingInstruction_Id37 = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<level-0>
  <level-1>
    <?hi id=""37""?>
    <level-42/>
  </level-1>
</level-0>";

        private static readonly string ProcessingInstruction_Id36 = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<level-0>
  <level-1>
    <?hi id=""36""?>
    <level-42/>
  </level-1>
</level-0>";

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqual()
        {
            var xDocumentA = XDocument.Parse(PersonXml, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(PersonXml, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestUnequal()
        {
            var xDocumentA = XDocument.Parse(PersonXml, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Mozart_Utf8, LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestDeclarationExclusion()
        {
            var xDocumentA = XDocument.Parse(Mozart_Utf8, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Mozart_8859_1, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { DeclarationHandling = XmlComperator.DeclarationHandling.Ignore }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestDeclarationInclusion()
        {
            var xDocumentA = XDocument.Parse(Mozart_Utf8, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Mozart_8859_1, LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { DeclarationHandling = XmlComperator.DeclarationHandling.Compare }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestTextStrict()
        {
            var xDocumentA = XDocument.Parse(Mozart_8859_1, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Mozart_8859_1_CData, LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { TextNodeHandling = XmlComperator.TextNodeHandling.Strict } ));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestCDataAsText()
        {
            var xDocumentA = XDocument.Parse(Mozart_8859_1, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Mozart_8859_1_CData, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { TextNodeHandling = XmlComperator.TextNodeHandling.CDataAsText }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestTextConcatenated()
        {
            var xDocumentA = XDocument.Parse(Mozart_8859_1_CData, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Mozard_8859_1_CData_Combined, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { TextNodeHandling = XmlComperator.TextNodeHandling.Concatenated }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesStrictCompliant()
        {
            var xDocumentA = XDocument.Parse(Detail_ABCD, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Detail_ABCD, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.Strict }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesStrictNonCompliant1()
        {
            var xDocumentA = XDocument.Parse(Detail_ABCD, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Detail_ACD, LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.Strict }));
        }
        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesStrictNonCompliant2()
        {
            var xDocumentA = XDocument.Parse(Detail_ABCD, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Detail_ACDB, LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.Strict }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnore()
        {
            var xDocumentA = XDocument.Parse(Detail_ACD, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Detail_EFG, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.Ignore }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnoreMissingInACompliant()
        {
            var xDocumentA = XDocument.Parse(Detail_BD, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Detail_ABCD, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.IgnoreMissingInA }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnoreMissingInANonCompliant()
        {
            var xDocumentA = XDocument.Parse(Detail_BED, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Detail_ABCD, LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.IgnoreMissingInA }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnoreMissingInBCompliant()
        {
            var xDocumentA = XDocument.Parse(Detail_ABCD, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Detail_BD, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.IgnoreMissingInB }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnoreMissingInBNonCompliant()
        {
            var xDocumentA = XDocument.Parse(Detail_ABCD, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Detail_BED, LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.IgnoreMissingInB }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnoreOrderCompliant()
        {
            var xDocumentA = XDocument.Parse(Detail_ABCD, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Detail_ACDB, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.IgnoreOrder }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnoreOrderNonCompliant()
        {
            var xDocumentA = XDocument.Parse(Detail_ABCD, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Detail_BED, LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.IgnoreOrder }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualCompareCommentsCompliant()
        {
            var xDocumentA = XDocument.Parse(Comment1, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Comment1, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { CommentHandling = XmlComperator.CommentHandling.Compare }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualCompareCommentsNonCompliant()
        {
            var xDocumentA = XDocument.Parse(Comment1, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Comment2, LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { CommentHandling = XmlComperator.CommentHandling.Compare }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualIgnoreCommentsCompliant()
        {
            var xDocumentA = XDocument.Parse(Comment2, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Comment2, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { CommentHandling = XmlComperator.CommentHandling.Ignore }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualIgnoreCommentsNonCompliant()
        {
            var xDocumentA = XDocument.Parse(Comment1, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(Comment2, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { CommentHandling = XmlComperator.CommentHandling.Ignore }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualCompareProcessingInstructionsCompliant()
        {
            var xDocumentA = XDocument.Parse(ProcessingInstruction_Id37, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(ProcessingInstruction_Id37, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { ProcessingInstructionHandling = XmlComperator.ProcessingInstructionHandling.Compare }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualCompareProcessingInstructionsNonCompliant()
        {
            var xDocumentA = XDocument.Parse(ProcessingInstruction_Id37, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(ProcessingInstruction_Id36, LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { ProcessingInstructionHandling = XmlComperator.ProcessingInstructionHandling.Compare }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualIgnoreProcessingInstructionsNonCompliant()
        {
            var xDocumentA = XDocument.Parse(ProcessingInstruction_Id37, LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Parse(ProcessingInstruction_Id36, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { ProcessingInstructionHandling = XmlComperator.ProcessingInstructionHandling.Ignore }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestInnerNamespaces()
        {
            XNamespace myNameSpace = "http://runerne.dk/Modules";
            var xDocumentA = new XDocument();
            xDocumentA.Add(
                new XElement("root", 
                    new XElement(myNameSpace + "child", "This is a test")
                )
            );

            var stream = new MemoryStream();
            xDocumentA.Save(stream);
            stream.Position = 0;
            var xDocumentB = XDocument.Load(stream, LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, XmlComperator.Relaxed));
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentB, xDocumentA, XmlComperator.Relaxed));
        }
    }
}
