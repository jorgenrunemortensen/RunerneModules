using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace Runerne.Utilities.UnitTest
{
    [TestClass]
    public class ValueUtilitiesTest
    {
        /*********/
        /* sbyte */
        /*********/

        [TestMethod]
        [TestCategory("ValueUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ParseIntegral_SByte_1()
        {
            var res = ValueUtilities.ParseIntegral("-1");
            Assert.AreEqual((sbyte)-1, res);
        }

        [TestMethod]
        [TestCategory("ValueUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ParseIntegral_SByte_2()
        {
            var res = ValueUtilities.ParseIntegral("127");
            Assert.AreEqual((sbyte)127, res);
        }

        [TestMethod]
        [TestCategory("ValueUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ParseIntegral_SByte_3()
        {
            var res = ValueUtilities.ParseIntegral("128");
            Assert.IsNotInstanceOfType(res, typeof(sbyte));
        }


        /********/
        /* byte */
        /********/

        [TestMethod]
        [TestCategory("ValueUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ParseIntegral_Byte_0()
        {
            var res = ValueUtilities.ParseIntegral("15");
            Assert.IsNotInstanceOfType(res, typeof(byte));
        }

        [TestMethod]
        [TestCategory("ValueUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ParseIntegral_Byte_1()
        {
            var res = ValueUtilities.ParseIntegral("128");
            Assert.AreEqual((byte)128, res);
        }

        [TestMethod]
        [TestCategory("ValueUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ParseIntegral_Byte_3()
        {
            var res = ValueUtilities.ParseIntegral("255");
            Assert.AreEqual((byte)255, res);
        }

        [TestMethod]
        [TestCategory("ValueUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ParseIntegral_Byte_4()
        {
            var res = ValueUtilities.ParseIntegral("1000");
            Assert.IsNotInstanceOfType(res, typeof(byte));
        }

        /*********/
        /* short */
        /*********/



        /**********/
        /* ushort */
        /**********/

        /**********/
        /* float  */
        /**********/

        [TestMethod]
        [TestCategory("ValueUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ParseDecimal_0()
        {
            var res = ValueUtilities.ParseDecimals("15,3");
            Assert.IsInstanceOfType(res, typeof(float));
            Assert.AreEqual((float)15.3, res);
        }


        /**********/
        /* double */
        /**********/


        /***********/
        /* minimum */
        /***********/

        [TestMethod]
        [TestCategory("ValueUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ParseIntegral_Byte_Minimum_Integer_1()
        {
            var res = ValueUtilities.ParseIntegral("100", typeof(int));
            Assert.IsInstanceOfType(res, typeof(int));
            Assert.AreEqual(100, res);
        }

        private static string GetDecimalSeparator()
        {
            return CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        }

        [TestMethod]
        [TestCategory("ValueUtilities")]
        [TestCategory("GatedCheckIn")]
        public void CastTo_Double_1a()
        {
            
            var res = ValueUtilities.CastTo($"3{GetDecimalSeparator()}14", typeof(double));
            Assert.IsInstanceOfType(res, typeof(double));
            Assert.AreEqual(3.14, res);
        }

        [TestMethod]
        [TestCategory("ValueUtilities")]
        [TestCategory("GatedCheckIn")]
        public void CastTo_Double_1b()
        {
            var res = ValueUtilities.CastTo<double>($"3{GetDecimalSeparator()}14");
            Assert.AreEqual(3.14, res);
        }
    }
}
