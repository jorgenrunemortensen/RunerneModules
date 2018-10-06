using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Runerne.Instantiation.UnitTest
{
    [TestClass]
    public class ContextTest
    {
        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]
        public void NullInstanceTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("A", new NullTypeInstanceProvider(typeof(string)));

            var s = context.GetInstance("A");
            Assert.IsNull(s);
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]
        public void NullInstanceTypeStrongTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("A", new NullTypeInstanceProvider(typeof(string)));

            var s = context.GetInstance<string>("A");
            Assert.IsNull(s);
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void NoNullInstanceTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("A", new NullTypeInstanceProvider(typeof(string)));

            try
            {
                var s = context.GetInstance("B");
                Assert.Fail("An InstanceNotFoundException was expected.");
            }
            catch (InstanceNotFoundException)
            {
            }
            catch (Exception)
            {
                Assert.Fail("Another exception than InstanceNotFoundException was not expected.");
            }
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void NoNullInstanceTypeStrongTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("A", new NullTypeInstanceProvider(typeof(string)));

            try
            {
                var s = context.GetInstance<int>("A");
                Assert.Fail("An IncompatibleInstanceTypeException was expected.");
            }
            catch (IncompatibleInstanceTypeException)
            {
            }
            catch (Exception)
            {
                Assert.Fail("Another exception than IncompatibleInstanceTypeException was not expected.");
            }
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void StringTypeTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("A", new SimpleInstanceProvider("This is a test!"));

            var s = context.GetInstance<string>("A");
            Assert.AreEqual("This is a test!", s);
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void BooleanTypeTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("0", new SimpleInstanceProvider(false));
            context.AddNamedInstanceProvider("1", new SimpleInstanceProvider(true));

            var falseValue = context.GetInstance<bool>("0");
            Assert.IsFalse(falseValue);

            var trueValue = context.GetInstance<bool>("1");
            Assert.IsTrue(trueValue);
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void ClassTypeTest()
        {
            var context = new RIContext();

            var nameInstanceProvider = new SimpleInstanceProvider("Jørgen");
            var ageInstanceProvider = new SimpleInstanceProvider(49);
            var genderInstanceProvider = new EnumInstanceProvider(Gender.Male, typeof(Gender));

            var dateOfBirth = new DateTime(1966, 7, 14);

            var classInstanceProvider = new ComplexInstanceProvider(
                typeof(Person),
                new IInstanceProvider[] {
                    nameInstanceProvider,
                    ageInstanceProvider,
                    genderInstanceProvider
                },
                new Dictionary<string, IInstanceProvider>()
                {
                    { "Birthday", new SimpleInstanceProvider(dateOfBirth) },
                }
            );
            context.AddNamedInstanceProvider("A", classInstanceProvider);

            var person = context.GetInstance<Person>("A");
            Assert.AreEqual("Jørgen", person.Name);

            var now = DateTime.Today;
            var age = now.Year - dateOfBirth.Year + (now.DayOfYear >= dateOfBirth.DayOfYear ? 0 : -1);
            Assert.AreEqual(age, person.Age);
            Assert.AreEqual(Gender.Male, person.Gender);
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void NonMathcingContructorClassTypeTest()
        {
            var context = new RIContext();

            var genderInstanceProvider = new EnumInstanceProvider(Gender.Male, typeof(Gender));
            var nameInstanceProvider = new SimpleInstanceProvider("Jørgen");
            try
            {
                var classInstanceProvider = new ComplexInstanceProvider(
                    typeof(Person),
                    new IInstanceProvider[] {
                        genderInstanceProvider,
                        nameInstanceProvider,
                    },
                    new Dictionary<string, IInstanceProvider>()
                    {
                        { "Birthday", new SimpleInstanceProvider(new DateTime(1966, 7, 14)) },
                    }
                );
                Assert.Fail("A NoMatchingConstructorException was expected.");
            }
            catch (NoMatchingConstructorException)
            {
            }
            catch (Exception)
            {
                Assert.Fail("Another exception than NoMatchingConstructorException was not expected.");
            }
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void NonMathcingPropertyTest()
        {
            var context = new RIContext();

            var nameInstanceProvider = new SimpleInstanceProvider("Jørgen");
            var ageInstanceProvider = new SimpleInstanceProvider(49);
            var genderInstanceProvider = new EnumInstanceProvider(Gender.Male, typeof(Gender));

            try
            {
                var classInstanceProvider = new ComplexInstanceProvider(
                    typeof(Person),
                    new IInstanceProvider[] {
                        nameInstanceProvider,
                        ageInstanceProvider,
                        genderInstanceProvider
                    },
                    new Dictionary<string, IInstanceProvider>()
                    {
                    { "Shoe Size", new SimpleInstanceProvider(44) },
                    }
                );
                Assert.Fail("A NoMatchingPropertyException was expected.");
            }
            catch (NoMatchingPropertyException)
            {
            }
            catch (Exception)
            {
                Assert.Fail("Another exception than NoMatchingPropertyException was not expected.");
            }
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void MathcingFieldTest()
        {
            var context = new RIContext();

            var classInstanceProvider = new ComplexInstanceProvider(
                typeof(Address),
                new IInstanceProvider[]
                {
                },
                new Dictionary<string, IInstanceProvider>()
                {
                    { "AddressLine1", new SimpleInstanceProvider("Vesterbygdvej 10") },
                    { "City", new SimpleInstanceProvider("Ølstykke") }
                }
            );
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void NonMathcingFieldTest()
        {
            var context = new RIContext();

            try
            {
                var classInstanceProvider = new ComplexInstanceProvider(
                    typeof(Address),
                    new IInstanceProvider[]
                    {
                    },
                    new Dictionary<string, IInstanceProvider>()
                    {
                        { "AddressLine1", new SimpleInstanceProvider("Vesterbygdvej 10") },
                        { "ZipCode", new SimpleInstanceProvider("3650") }
                    }
                );
                Assert.Fail("A NoMatchingPropertyException was expected.");
            }
            catch (NoMatchingPropertyException)
            {
            }
            catch (Exception)
            {
                Assert.Fail("Another exception than NoMatchingPropertyException was not expected.");
            }
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void DuplicatedNameTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("TheName", null);
            try
            {
                context.AddNamedInstanceProvider("TheName", null);
                Assert.Fail("A DuplicateInstanceNameException was expected thrown.");
            }
            catch (DuplicateInstanceNameException ex)
            {
                Assert.AreEqual("TheName", ex.Name);
            }
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void NoSuchNameTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("TheName", null);
            try
            {
                context.GetInstance("Kurt");
                Assert.Fail("A InstanceNotFoundException was expected thrown.");
            }
            catch (InstanceNotFoundException ex)
            {
                Assert.AreEqual("Kurt", ex.InstanceName);
            }
        }

        [TestMethod]
        [TestCategory("Context")]
        [TestCategory("GatedCheckIn")]

        public void NoSuchInstanceProviderTest()
        {
            var context = new RIContext();
            context.AddNamedInstanceProvider("TheName", null);
            try
            {
                context.GetInstanceProvider("Kurt");
                Assert.Fail("A NoSuchInstanceProviderException was expected thrown.");
            }
            catch (NoSuchInstanceProviderException ex)
            {
                Assert.AreEqual("Kurt", ex.Name);
            }
        }
    }
}