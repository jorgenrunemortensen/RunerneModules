using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class ParamsTest
    {
        [TestMethod]
        [TestCategory("Xml")]
        [TestCategory("Params")]
        [TestCategory("GatedCheckIn")]
        public void TestParams()
        {
            var context = XmlLoader.Load("..\\..\\TestFiles\\Params-Test.xml");
            var instance = context.GetInstance<ParamsClass>("A");
            var text = instance.GetText();
            Assert.AreEqual("36: One, Two", text);
        }
    }
}
