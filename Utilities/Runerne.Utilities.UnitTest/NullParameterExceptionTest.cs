using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Utilities.UnitTest
{
    [TestClass]
    public class NullParameterExceptionTest
    {
        [TestMethod]
        [TestCategory("NullParameterException")]
        [TestCategory("GatedCheckIn")]
        public void NullParameterException1()
        {
            var exception = new NullParameterException("The parameter name");
            Assert.AreEqual("The parameter name", exception.ParameterName);
            Assert.AreEqual("The parameter 'The parameter name' cannot be null.", exception.Message);
        }
    }
}
