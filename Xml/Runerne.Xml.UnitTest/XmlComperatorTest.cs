using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Xml.UnitTest
{
    [TestClass]
    public class XmlComperatorTest
    {
        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqual()
        {
            var xDocumentA = XDocument.Load("1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("1.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestUnequal()
        {
            var xDocumentA = XDocument.Load("1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("2.xml", LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestDeclarationExclusion()
        {
            var xDocumentA = XDocument.Load("Declaration-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Declaration-2.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { DeclarationHandling = XmlComperator.DeclarationHandling.Ignore }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestDeclarationInclusion()
        {
            var xDocumentA = XDocument.Load("Declaration-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Declaration-2.xml", LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { DeclarationHandling = XmlComperator.DeclarationHandling.Compare }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestTextStrict()
        {
            var xDocumentA = XDocument.Load("Text-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Text-2.xml", LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { TextNodeHandling = XmlComperator.TextNodeHandling.Strict } ));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestCDataAsText()
        {
            var xDocumentA = XDocument.Load("Text-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Text-2.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { TextNodeHandling = XmlComperator.TextNodeHandling.CDataAsText }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestTextConcatenated()
        {
            var xDocumentA = XDocument.Load("Text-2.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Text-3.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { TextNodeHandling = XmlComperator.TextNodeHandling.Concatenated }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesStrictCompliant()
        {
            var xDocumentA = XDocument.Load("Attributes-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Attributes-2.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.Strict }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesStrictNonCompliant1()
        {
            var xDocumentA = XDocument.Load("Attributes-2.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Attributes-3.xml", LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.Strict }));
        }
        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesStrictNonCompliant2()
        {
            var xDocumentA = XDocument.Load("Attributes-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Attributes-7.xml", LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.Strict }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnore()
        {
            var xDocumentA = XDocument.Load("Attributes-3.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Attributes-4.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.Ignore }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnoreMissingInACompliant()
        {
            var xDocumentA = XDocument.Load("Attributes-5.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Attributes-1.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.IgnoreMissingInA }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnoreMissingInANonCompliant()
        {
            var xDocumentA = XDocument.Load("Attributes-6.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Attributes-1.xml", LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.IgnoreMissingInA }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnoreMissingInBCompliant()
        {
            var xDocumentA = XDocument.Load("Attributes-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Attributes-5.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.IgnoreMissingInB }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnoreMissingInBNonCompliant()
        {
            var xDocumentA = XDocument.Load("Attributes-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Attributes-6.xml", LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.IgnoreMissingInB }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnoreOrderCompliant()
        {
            var xDocumentA = XDocument.Load("Attributes-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Attributes-7.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.IgnoreOrder }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestAttributesIgnoreOrderNonCompliant()
        {
            var xDocumentA = XDocument.Load("Attributes-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Attributes-6.xml", LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { AttributeHandling = XmlComperator.AttributeHandling.IgnoreOrder }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualCompareCommentsCompliant()
        {
            var xDocumentA = XDocument.Load("Comment-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Comment-1.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { CommentHandling = XmlComperator.CommentHandling.Compare }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualCompareCommentsNonCompliant()
        {
            var xDocumentA = XDocument.Load("Comment-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Comment-2.xml", LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { CommentHandling = XmlComperator.CommentHandling.Compare }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualIgnoreCommentsCompliant()
        {
            var xDocumentA = XDocument.Load("Comment-2.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Comment-2.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { CommentHandling = XmlComperator.CommentHandling.Ignore }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualIgnoreCommentsNonCompliant()
        {
            var xDocumentA = XDocument.Load("Comment-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Comment-2.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { CommentHandling = XmlComperator.CommentHandling.Ignore }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualCompareProcessingInstructionsCompliant()
        {
            var xDocumentA = XDocument.Load("Processing-Instruction-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Processing-Instruction-1.xml", LoadOptions.SetLineInfo);
            Assert.IsTrue(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { ProcessingInstructionHandling = XmlComperator.ProcessingInstructionHandling.Compare }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualCompareProcessingInstructionsNonCompliant()
        {
            var xDocumentA = XDocument.Load("Processing-Instruction-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Processing-Instruction-2.xml", LoadOptions.SetLineInfo);
            Assert.IsFalse(XmlComperator.AreEqual(xDocumentA, xDocumentB, new XmlComperator.Options { ProcessingInstructionHandling = XmlComperator.ProcessingInstructionHandling.Compare }));
        }

        [TestMethod]
        [TestCategory("Xml Comperator")]
        [TestCategory("GatedCheckIn")]
        public void TestEqualIgnoreProcessingInstructionsNonCompliant()
        {
            var xDocumentA = XDocument.Load("Processing-Instruction-1.xml", LoadOptions.SetLineInfo);
            var xDocumentB = XDocument.Load("Processing-Instruction-2.xml", LoadOptions.SetLineInfo);
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
