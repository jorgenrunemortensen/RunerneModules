using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Runerne.Utilities.UnitTest
{
    [TestClass]
    public class StringUtilitiesTest
    {
        private static readonly string _mollyMalone =
            "In Dublin's fair city,\n" +
            "Where the girls are so pretty,\n" +
            "I first set my eyes on sweet Molly Malone,\n" +
            "As she wheeled her wheel-barrow,\n" +
            "Through streets broad and narrow,\n" +
            "Crying, \"Cockles and mussels, alive, alive, oh!\"";

        [TestMethod]
        [TestCategory("StringUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void LineLead()
        {
            var result = StringUtilities.LineLead("Lyric ", _mollyMalone);

            var expected = $"Lyric In Dublin's fair city,{Environment.NewLine}" +
                $"Lyric Where the girls are so pretty,{Environment.NewLine}" +
                $"Lyric I first set my eyes on sweet Molly Malone,{Environment.NewLine}" +
                $"Lyric As she wheeled her wheel-barrow,{Environment.NewLine}" +
                $"Lyric Through streets broad and narrow,{Environment.NewLine}" +
                $"Lyric Crying, \"Cockles and mussels, alive, alive, oh!\"";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("StringUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void ConvertType_String()
        {
            var result = StringUtilities.ConvertString<string>("This is a test");
            Assert.IsInstanceOfType(result, typeof(string));
            Assert.AreEqual("This is a test", result);
        }

        [TestMethod]
        [TestCategory("StringUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void ConvertType_Boolean_True()
        {
            var result = StringUtilities.ConvertString<bool>("TRUE");
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("StringUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void AddTextBlock_EmptyTextBuffer()
        {
            var result = StringUtilities.AddTextBlock("  ", _mollyMalone);
            Assert.AreEqual(_mollyMalone, result);
        }

        [TestMethod]
        [TestCategory("StringUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void AddTextBlock_SingleLineTextBuffer()
        {
            var result = StringUtilities.AddTextBlock("The Dubliners.", _mollyMalone);
            Assert.AreEqual($"{_mollyMalone}{Environment.NewLine}{Environment.NewLine}The Dubliners.", result);
        }

        [TestMethod]
        [TestCategory("StringUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void AddTextBlock_MultipleLineTextBuffer()
        {
            var result = StringUtilities.AddTextBlock($"The Dubliners.{Environment.NewLine}Live From The Gaiety.", _mollyMalone);
            Assert.AreEqual($"{_mollyMalone}{Environment.NewLine}{Environment.NewLine}The Dubliners.{Environment.NewLine}Live From The Gaiety.", result);
        }
    }
}