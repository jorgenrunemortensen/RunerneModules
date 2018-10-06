using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Runerne.Computation.DateTime;
using Runerne.Computation.String;

namespace Runerne.Computation.UnitTest
{
    [TestClass]
    public class DateTimeTest
    {
        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void AddTimeSpansTest()
        {
            var ts1 = new TimeSpanConstant(new TimeSpan(10, 8, 8));
            var ts2 = new TimeSpanConstant(new TimeSpan(0, 18, 59));
            var ts3 = new TimeSpanConstant(new TimeSpan(15, 50, 36));

            var adder = new AddTimeSpans(ts1, ts2, ts3);

            Assert.AreEqual(new TimeSpan(26, 17, 43), adder.Value);
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void AddToDateTimeTest()
        {
            var ts = new TimeSpanConstant(new TimeSpan(5, 30, 11));
            var dt = new DateTimeConstant(new System.DateTime(1966, 7, 14, 13, 57, 15));

            var adder = new AddToDateTime(dt, ts);

            Assert.AreEqual(new System.DateTime(1966, 7, 14, 19, 27, 26), adder.Value);
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void DateTimeDifferenceTest()
        {
            var dt1 = new DateTimeConstant(new System.DateTime(1966, 4, 28));
            var dt2 = new DateTimeConstant(new System.DateTime(1966, 7, 14));

            var differ = new DateTimeDifference(dt1, dt2);

            Assert.AreEqual(new TimeSpan(77, 0, 0, 0, 0), differ.Value);
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void DateTimeTicksTest()
        {
            var dt = new DateTimeConstant(new System.DateTime(2017, 10, 18, 11, 18, 5));
            var ticks = new DateTimeTicks(dt);
            Assert.AreEqual(636439222850000000, ticks.Value);
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void NegateTimeSpanTest()
        {
            var ts = new TimeSpanConstant(new TimeSpan(15, 10, 5));
            var nts = new NegateTimeSpan(ts);

            Assert.AreEqual(new TimeSpan(-15, -10, -5), nts.Value);
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void NowTest1()
        {
            var dt = new DateTimeConstant(System.DateTime.Now);
            // One tick is 1E-7 seconds

            var diff = Math.Abs(dt.Value.Ticks - System.DateTime.Now.Ticks);

            Assert.IsTrue(diff < 100000); // Difference is less than 100ms
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void NowTest2()
        {
            var dt = new Now();
            // One tick is 1E-7 seconds

            var diff = Math.Abs(dt.Value.Ticks - System.DateTime.Now.Ticks);

            Assert.IsTrue(diff < 100000); // Difference is less than 100ms
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void TimeSpanTest()
        {
            var ts = new TimeSpanTikcs(new TimeSpanConstant(new TimeSpan(10000)));
            Assert.AreEqual(10000, ts.Value);
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void ParseDateTimeDateOkTest()
        {
            var isValidDateTime = new IsValidDateTime(new StringConstant("2018-02-28"), "yyyy-MM-dd");
            Assert.IsTrue(isValidDateTime.Value);
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void ParseDateTimeUnexistingDateTest()
        {
            var isValidDateTime = new IsValidDateTime(new StringConstant("2018-02-29"), "yyyy-MM-dd");
            Assert.IsFalse(isValidDateTime.Value);
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void ParseDateTimeTooBigTest()
        {
            var isValidDateTime = new IsValidDateTime(new StringConstant("10000-02-20"), "yyyy-MM-dd");
            Assert.IsFalse(isValidDateTime.Value);
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void DateTimeVariableInitialValueTest()
        {
            var dateTime = System.DateTime.Now;
            var variable = new DateTimeVariable(dateTime);
            Assert.AreEqual(dateTime, variable.Value);
        }

        [TestMethod]
        [TestCategory("GatedCheckIn")]
        [TestCategory("DateTime")]
        public void DateTimeVariableChangeValueTest()
        {
            var variable = new DateTimeVariable(System.DateTime.Now);
            var newDateTime = System.DateTime.Parse("1966-07-14");
            variable.Value = newDateTime;
            Assert.AreEqual(newDateTime, variable.Value);
        }
    }
}