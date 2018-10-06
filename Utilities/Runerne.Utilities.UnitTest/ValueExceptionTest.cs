using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Utilities.UnitTest
{
    public class ValueExceptionTest
    {
        [TestMethod]
        [TestCategory("ValueException")]
        [TestCategory("GatedCheckIn")]
        public void ValueException1()
        {
            var exception = new ValueException("The message");
            Assert.AreEqual("The message", exception.Message);
        }
    }
}
