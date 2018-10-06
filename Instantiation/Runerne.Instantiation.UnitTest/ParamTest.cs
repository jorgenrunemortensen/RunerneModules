using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Instantiation.UnitTest
{
    [TestClass]
    public class ParamTest
    {
        class TestClass
        {
            private int _n;
            private string[] _messages;

            public TestClass(int n, params string[] messages)
            {
                _n = n;
                _messages = messages;
            }

            public string GetText()
            {
                return _n.ToString() + " " + string.Join(", ", _messages);
            }
        }

        [TestMethod]
        [TestCategory("Params")]
        [TestCategory("GatedCheckIn")]

        public void TestTwoParams()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("A", new ComplexInstanceProvider(
                typeof(TestClass),
                new IInstanceProvider[] {
                    new SimpleInstanceProvider(36),
                    new ListInstanceProvider(
                        new IInstanceProvider[]
                        {
                            new SimpleInstanceProvider("Text1"),
                            new SimpleInstanceProvider("Text2"),
                        },
                        ListInstanceProvider.CollectionType.Array
                    ),
                },
                new Dictionary<string, IInstanceProvider>() { }
                )
            );
            var instance = context.GetInstance<TestClass>("A");
            var text = instance.GetText();
            Assert.AreEqual("36 Text1, Text2", text);
        }
    }
}
