using Microsoft.VisualStudio.TestTools.UnitTesting;
using Runerne.Utilities;

namespace WorkZone.Utilities.Test.Unit
{
    [TestClass]
    public class ParseExceptionTest
    {
        [TestMethod]
        [TestCategory("StringUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void ParseException1()
        {
            var exception = new ParseException("<Value>", "<Reason>");
            Assert.AreEqual("<Value>", exception.Value);
            Assert.AreEqual("<Reason>", exception.Reason);
            Assert.AreEqual("Unable to parse '<Value>'. <Reason>", exception.Message);
        }
    }
}
