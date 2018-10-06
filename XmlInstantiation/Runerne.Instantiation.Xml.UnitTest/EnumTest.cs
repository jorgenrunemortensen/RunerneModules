using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class EnumTest
    {
        [TestMethod]
        [TestCategory("SimpleTypes")]
        [TestCategory("Xml")]
        [TestCategory("GatedCheckIn")]
        public void EnumTest1()
        {
            var context = XmlLoader.Load("..\\..\\TestFiles\\Enum-Test.xml");
            var mood = context.GetInstance<Color>("Mood");
            Assert.IsNotNull(mood);
            Assert.IsInstanceOfType(mood, typeof(Color));
            Assert.AreEqual(Color.Green, mood);
        }
    }
}
