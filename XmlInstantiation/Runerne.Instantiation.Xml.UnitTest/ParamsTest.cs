using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.Xml.UnitTest
{
    [TestClass]
    public class ParamsTest
    {
        private static readonly string ParamsXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<context xmlns=""http://runerne.dk/Instantiation.Xml"">
  <includes>
    <include>Runerne.Instantiation.dll</include>
    <include>Runerne.Instantiation.Xml.UnitTest.dll</include>
  </includes>
  <instances>
    <complex-instance name=""A"" class=""Runerne.Instantiation.Xml.UnitTest.ParamsClass"">
      <constructor-args>
        <simple-instance type=""int"">36</simple-instance>
        <list as=""array"" element-type=""string"">
          <simple-instance>One</simple-instance>
          <simple-instance>Two</simple-instance>
        </list>
      </constructor-args>
    </complex-instance>
  </instances>
</context>";

        [TestMethod]
        [TestCategory("Xml")]
        [TestCategory("Params")]
        [TestCategory("GatedCheckIn")]
        public void TestParams()
        {
            var context = XmlLoader.Parse(ParamsXml);
            var instance = context.GetInstance<ParamsClass>("A");
            var text = instance.GetText();
            Assert.AreEqual("36: One, Two", text);
        }
    }
}
