using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Utilities.UnitTest
{
    [TestClass]
    public class BooleanUtilitiesTest
    {
        [TestMethod]
        [TestCategory("BooleanUtilities")]
        [TestCategory("GatedCheckIn")]
        public void FalseToBool()
        {
            foreach (var token in BooleanUtilities.FalseTokens)
            {
                Assert.IsFalse(BooleanUtilities.ToBool(token));
            }
        }

        [TestMethod]
        [TestCategory("BooleanUtilities")]
        [TestCategory("GatedCheckIn")]
        public void TrueToBool()
        {
            foreach (var token in BooleanUtilities.TrueTokens)
            {
                Assert.IsTrue(BooleanUtilities.ToBool(token));
            }
        }

        [TestMethod]
        [TestCategory("BooleanUtilities")]
        [TestCategory("GatedCheckIn")]
        public void FailingToBool()
        {
            try
            {
                BooleanUtilities.ToBool("An invalid value");
                Assert.Fail("An exception was expected to be thrown.");
            }
            catch (ParseException)
            {
            }
        }
    }
}
