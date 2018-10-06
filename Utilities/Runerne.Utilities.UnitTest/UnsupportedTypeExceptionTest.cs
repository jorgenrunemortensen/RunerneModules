using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Utilities.UnitTest
{
    [TestClass]
    public class UnsupportedTypeExceptionTest
    {
        [TestMethod]
        public void UnsupportedTypeException1()
        {
            var exception = new UnsupportedTypeException("A value", "A type");
            Assert.AreEqual("A value", exception.Value);
            Assert.AreEqual("A type", exception.TypeName);
            Assert.AreEqual("Unable to parse 'A value'. The type 'A type' is not supported.", exception.Message);
        }
    }
}
