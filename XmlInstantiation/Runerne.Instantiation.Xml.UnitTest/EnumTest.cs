using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class EnumTest
    {
        private static readonly string EnumXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<context xmlns=""http://runerne.dk/Instantiation.Xml"">
  <includes>
  </includes>
  <instances>
    <simple-instance name=""Mood"" type=""Runerne.Instantiation.Xml.UnitTest.Color"">Green</simple-instance>
  </instances>
</context>
  ";

        [TestMethod]
        [TestCategory("SimpleTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void EnumTest1()
        {
            var context = XmlLoader.Parse(EnumXml);
            var mood = context.GetInstance<Color>("Mood");
            Assert.IsNotNull(mood);
            Assert.IsInstanceOfType(mood, typeof(Color));
            Assert.AreEqual(Color.Green, mood);
        }
    }
}
