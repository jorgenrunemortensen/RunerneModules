using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Utilities.UnitTest
{
    [TestClass]
    public class TextFormatterTest
    {
        private TextFormatter _sut;

        private void Given_A_TextFormatter()
        {
            _sut = new TextFormatter();
        }

        [TestMethod]
        [TestCategory("Text Formatter")]
        [TestCategory("GatedCheckIn")]
        public void SingleLineTabbingNoTabs()
        {
            var input = "This is a test";
            Given_A_TextFormatter();
            var result = _sut.FormatTextBlock(input);
            Assert.AreEqual("This is a test", result);
        }

        [TestMethod]
        [TestCategory("Text Formatter")]
        [TestCategory("GatedCheckIn")]
        public void MultiLineTabbingNoTabs()
        {
            var input = "This is a test" + Environment.NewLine + "which spans over two lines.";
            Given_A_TextFormatter();
            var result = _sut.FormatTextBlock(input);
            Assert.AreEqual("This is a test" + Environment.NewLine + "which spans over two lines.", result);
        }

        [TestMethod]
        [TestCategory("Text Formatter")]
        [TestCategory("GatedCheckIn")]
        public void SingleLineTabbingSingleTabLeft()
        {
            var input = "This is a \ttest";
            Given_A_TextFormatter();
            _sut.SetTab(5);
            var result = _sut.FormatTextBlock(input);
            Assert.AreEqual("This test", result);
        }

        [TestMethod]
        [TestCategory("Text Formatter")]
        [TestCategory("GatedCheckIn")]
        public void MultiLineTabbingSingleTabLeft()
        {
            var input = "This is a \ttest" + Environment.NewLine + "that spans over two lines.";
            Given_A_TextFormatter();
            _sut.SetTab(5);
            var result = _sut.FormatTextBlock(input);
            Assert.AreEqual("This test" + Environment.NewLine + "     that spans over two lines.", result);
        }

        [TestMethod]
        [TestCategory("Text Formatter")]
        [TestCategory("GatedCheckIn")]
        public void SingleLineTabbingSingleTabRight()
        {
            var input = "This is a \ttest";
            Given_A_TextFormatter();
            _sut.SetTab(12);
            var result = _sut.FormatTextBlock(input);
            Assert.AreEqual("This is a   test", result);
        }

        [TestMethod]
        [TestCategory("Text Formatter")]
        [TestCategory("GatedCheckIn")]
        public void MultiLineTabbingSingleTabRight()
        {
            var input = "This is a \ttest" + Environment.NewLine + "that spans over two lines.";
            Given_A_TextFormatter();
            _sut.SetTab(12);
            var result = _sut.FormatTextBlock(input);
            Assert.AreEqual("This is a   test" + Environment.NewLine + "            that spans over two lines.", result);
        }

        [TestMethod]
        [TestCategory("Text Formatter")]
        [TestCategory("GatedCheckIn")]
        public void SingleLineTabbingMoreTabsThanDefined()
        {
            var input = "This is a \ttest \thas mulitple tabs.";
            Given_A_TextFormatter();
            _sut.SetTab(5);
            var result = _sut.FormatTextBlock(input);
            Assert.AreEqual("This test has mulitple tabs.", result);
        }

        [TestMethod]
        [TestCategory("Text Formatter")]
        [TestCategory("GatedCheckIn")]
        public void MultiLineTabbingMoreTabsThanDefined()
        {
            var input = "This is a \ttest \thas mulitple tabs" + Environment.NewLine + "and three lines." + Environment.NewLine + "This is the last line.";
            Given_A_TextFormatter();
            _sut.SetTab(5);
            var result = _sut.FormatTextBlock(input);
            Assert.AreEqual("This test has mulitple tabs" + Environment.NewLine + "     and three lines." + Environment.NewLine + "     This is the last line.", result);
        }

        [TestMethod]
        [TestCategory("Text Formatter")]
        [TestCategory("GatedCheckIn")]
        public void TabsAreMemorized()
        {
            Given_A_TextFormatter();
            _sut.SetTab(10);
            _sut.SetTab(30);
            _sut.SetTab(20);

            var tabs = _sut.Tabs.ToArray();
            Assert.AreEqual(3, tabs.Length);

            Assert.IsTrue(_sut.Tabs.Any(o => o.Position == 10));
            Assert.IsTrue(_sut.Tabs.Any(o => o.Position == 20));
            Assert.IsTrue(_sut.Tabs.Any(o => o.Position == 30));
        }

        [TestMethod]
        [TestCategory("Text Formatter")]
        [TestCategory("GatedCheckIn")]
        public void TabsAt()
        {
            Given_A_TextFormatter();
            _sut.SetTab(10);
            _sut.SetTab(20);
            _sut.SetTab(20);

            Assert.AreEqual(0, _sut.TabsAt(5).Count());
            Assert.AreEqual(1, _sut.TabsAt(10).Count());
            Assert.AreEqual(2, _sut.TabsAt(20).Count());
        }

        [TestMethod]
        [TestCategory("Text Formatter")]
        [TestCategory("GatedCheckIn")]
        public void ClearTabs()
        {
            Given_A_TextFormatter();
            _sut.SetTab(10);
            _sut.SetTab(20);
            _sut.SetTab(20);
            Assert.AreEqual(3, _sut.Tabs.Count());

            var tab10 = _sut.Tabs.Where(o => o.Position == 10).ToArray()[0];
            var tab20_1 = _sut.Tabs.Where(o => o.Position == 20).ToArray()[0];
            var tab20_2 = _sut.Tabs.Where(o => o.Position == 20).ToArray()[1];

            _sut.ClearTabs(new TextFormatter.Tab[] { new TextFormatter.Tab(5), tab10 });
            Assert.AreEqual(2, _sut.Tabs.Count());

            _sut.ClearTabs(new TextFormatter.Tab[] { tab20_1 });
            Assert.AreEqual(1, _sut.Tabs.Count());
        }
    }
}
