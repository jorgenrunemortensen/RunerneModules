﻿using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class XmlLoaderTest
    {

        private static readonly string ListXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<context xmlns=""http://runerne.dk/Instantiation.Xml"">
  <includes>
    <include>Runerne.Instantiation.dll</include>
    <include>Runerne.Instantiation.Xml.UnitTest.dll</include>
  </includes>
  
  <instances>

    <complex-instance name=""Class 1"" class=""Runerne.Instantiation.Xml.UnitTest.ClassWithListConstructor"">
      <constructor-args>
        <list element-type=""string"">
          <simple-instance>Hejsa</simple-instance>
        </list>
      </constructor-args>
    </complex-instance>

    
    <list name=""List 1"">
      <simple-instance>Cow</simple-instance>
      <simple-instance>Fox</simple-instance>
      <simple-instance>3,14</simple-instance>
      <simple-instance>Cat</simple-instance>
      <list>
        <simple-instance>0</simple-instance>
        <simple-instance>1</simple-instance>
        <simple-instance>2</simple-instance>
      </list>
    </list>

    <list name=""List 2"" element-type=""string"">
      <simple-instance>0</simple-instance>
      <simple-instance>3,14</simple-instance>
    </list>

    <list name=""List 3"" element-type=""double"">
      <simple-instance>0</simple-instance>
      <simple-instance>3,14</simple-instance>
      <!-- <simple-instance>Horse</simple-instance> -->
    </list>    
    

  </instances>
</context>
";

        [TestMethod]
        [TestCategory("XML Loader")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void XDocumentLoadTest()
        {
            var xDocument = XDocument.Parse(ListXml);
            XmlLoader.Parse(xDocument);
        }

        [TestMethod]
        [TestCategory("XML Loader")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void XmlParseTest()
        {
            XmlLoader.Parse(ListXml);
        }
    }
}
