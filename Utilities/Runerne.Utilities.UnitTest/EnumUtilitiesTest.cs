using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Runerne.Utilities.UnitTest
{
    [TestClass]
    public class EnumUtilitiesTest
    {
        private enum Animal
        {
            Dog,
            Cat,
            Fish,
            Cow,
        };

        private readonly IDictionary<string, Animal> Dictionary = new Dictionary<string, Animal>()
        {
            { "Hund", Animal.Dog },
            { "Kat", Animal.Cat },
            { "Ko", Animal.Cow },
            { "Fisk", Animal.Fish },
            { "A swimming creature", Animal.Fish },
        };


        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToText_DefaultUpper()
        {
            var s = EnumUtilities.ToText(Animal.Cat);
            Assert.AreEqual("Cat", s);
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToText_NoForceUpper()
        {
            var s = EnumUtilities.ToText(Animal.Fish, false);
            Assert.AreEqual("Fish", s);
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToText_ForceUpper()
        {
            var s = EnumUtilities.ToText(Animal.Cow, true);
            Assert.AreEqual("COW", s);
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToValue1_DefaultCaseControl_1()
        {
            var value = EnumUtilities.ToValue<Animal>("Dog");
            Assert.AreEqual(Animal.Dog, value);
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToValue1_DefaultCaseControl_2()
        {
            var value = EnumUtilities.ToValue<Animal>("dog");
            Assert.AreEqual(Animal.Dog, value);
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToValue1_DefaultCaseControl_3()
        {
            var value = EnumUtilities.ToValue<Animal>("dOG");
            Assert.AreEqual(Animal.Dog, value);
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToValue1_IgnoreCase_1()
        {
            var value = EnumUtilities.ToValue<Animal>("Dog", true);
            Assert.AreEqual(Animal.Dog, value);
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToValue1_IgnoreCase_2()
        {
            var value = EnumUtilities.ToValue<Animal>("dog", true);
            Assert.AreEqual(Animal.Dog, value);
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToValue1_IgnoreCase_3()
        {
            var value = EnumUtilities.ToValue<Animal>("dOG", true);
            Assert.AreEqual(Animal.Dog, value);
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToValue1_CaseSensitive_1()
        {
            var value = EnumUtilities.ToValue<Animal>("Dog", false);
            Assert.AreEqual(Animal.Dog, value);
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToValue1_CaseSensitive_2()
        {
            try
            {
                var value = EnumUtilities.ToValue<Animal>("dog", false);
                Assert.Fail("An exception was expected to be thrown");
            }
            catch(Exception ex)
            {
                Assert.AreEqual("Unable to convert \"dog\" into a Runerne.Utilities.UnitTest.EnumUtilitiesTest+Animal. \"dog\" is not a member of the enumeration. The following options are available: Dog, Cat, Fish, Cow.", ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToValue2()
        {
            Assert.AreEqual(Animal.Dog, EnumUtilities.ToValue("Hund", Dictionary));
            Assert.AreEqual(Animal.Cat, EnumUtilities.ToValue("Kat", Dictionary));
            Assert.AreEqual(Animal.Fish, EnumUtilities.ToValue("A swimming creature", Dictionary));

            try
            {
                EnumUtilities.ToValue("Bird", Dictionary);
            }
            catch(Exception ex)
            {
                Assert.AreEqual("Unable to map \"Bird\" into a Runerne.Utilities.UnitTest.EnumUtilitiesTest+Animal. Please use one of the following options: Hund, Kat, Ko, Fisk, A swimming creature.", ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToNullableValue1_WithValue()
        {
            var result = EnumUtilities.ToNullableValue<Animal>("Cat");
            result = null;
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToNullableValue1_WithNull()
        {
            var result = EnumUtilities.ToNullableValue<Animal>(null);
            result = null;
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToNullableValue2_WithValue()
        {
            var result = EnumUtilities.ToNullableValue<Animal>("Fisk", Dictionary);
            result = null;
        }

        [TestMethod]
        [TestCategory("EnumUtilities")]
        [TestCategory("GatedCheckIn")]
        public void ToNullableValue2_WithNull()
        {
            var result = EnumUtilities.ToNullableValue<Animal>(null, Dictionary);
            result = null;
        }

    }
}
