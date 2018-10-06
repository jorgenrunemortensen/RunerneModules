using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Runerne.Utilities.UnitTest
{
    [TestClass]
    public class DateTimeUtilitiesTest
    {
        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void FloorDateTimeNow()
        {
            var sut = DateTime.Now;
            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Milliseconds, 250);
                var expected = new DateTime(sut.Year, sut.Month, sut.Day, sut.Hour, sut.Minute, sut.Second, 250 * (sut.Millisecond / 250));
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Seconds, 5);
                var expected = new DateTime(sut.Year, sut.Month, sut.Day, sut.Hour, sut.Minute, 5 * (sut.Second / 5));
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Minutes, 10);
                var expected = new DateTime(sut.Year, sut.Month, sut.Day, sut.Hour, 10 * (sut.Minute / 10), 0);
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Hours, 3);
                var expected = new DateTime(sut.Year, sut.Month, sut.Day, 3 * (sut.Hour / 3), 0, 0);
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Days, 7);
                var expected = new DateTime(sut.Year, sut.Month, 1 + 7 * ((sut.Day - 1) / 7));
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Months, 3);
                var expected = new DateTime(sut.Year, 1 + 3 * ((sut.Month - 1) / 3), 1);
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Years, 10);
                var expected = new DateTime(10 * (sut.Year / 10), 1, 1);
                Assert.AreEqual(expected, flooredDateTime);
            }
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void FloorDateTimeHardTest()
        {
            var sut = new DateTime(1966, 7, 14, 13, 46, 15, 316);
            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Milliseconds, 250);
                var expected = new DateTime(1966, 7, 14, 13, 46, 15, 250);
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Seconds, 5);
                var expected = new DateTime(1966, 7, 14, 13, 46, 15);
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Minutes, 10);
                var expected = new DateTime(1966, 7, 14, 13, 40, 0);
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Hours, 3);
                var expected = new DateTime(1966, 7, 14, 12, 0, 0);
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Days, 7);
                var expected = new DateTime(1966, 7, 8, 0, 0, 0);
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Months, 3);
                var expected = new DateTime(1966, 7, 1, 0, 0, 0);
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                var flooredDateTime = DateTimeUtilities.FloorDateTime(sut, DateTimePart.Years, 10);
                var expected = new DateTime(1960, 1, 1, 0, 0, 0);
                Assert.AreEqual(expected, flooredDateTime);
            }

            {
                try
                {
                    DateTimeUtilities.FloorDateTime(sut, DateTimePart.Weeks, 1);
                    Assert.Fail("An exception should have been thrown.");
                }
                catch (NotSupportedException)
                {
                }
                catch (Exception ex)
                {
                    Assert.Fail($"The {ex.GetType()} type of exception was not expected!");
                }
            }
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetFirstStartDate_EmptySet()
        {
            var dateTimes = new DateTime?[] { };
            var first = DateTimeUtilities.GetFirstStartDate(dateTimes);
            Assert.IsNull(first);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetFirstStartDate_OnlyNull()
        {
            var dateTimes = new DateTime?[] { null };
            var first = DateTimeUtilities.GetFirstStartDate(dateTimes);
            Assert.IsNull(first);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetFirstStartDate_SetWithNull()
        {
            var date1 = new DateTime(1966, 7, 14);
            var date2 = new DateTime(2018, 10, 5);
            var dateTimes = new DateTime?[] { date1, date2, null };
            var first = DateTimeUtilities.GetFirstStartDate(dateTimes);
            Assert.IsNull(first);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetFirstStartDate_SetWithoutNull()
        {
            var date1 = new DateTime(1966, 7, 14);
            var date2 = new DateTime(2018, 10, 5);
            var dateTimes = new DateTime?[] { date1, date2 };
            var first = DateTimeUtilities.GetFirstStartDate(dateTimes);
            Assert.AreEqual(date1, first);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetLastStartDate_EmptySet()
        {
            var dateTimes = new DateTime?[] { };
            var last = DateTimeUtilities.GetLastStartDate(dateTimes);
            Assert.IsNull(last);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetLastStartDate_OnlyNull()
        {
            var dateTimes = new DateTime?[] { null };
            var last = DateTimeUtilities.GetLastStartDate(dateTimes);
            Assert.IsNull(last);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetLastStartDate_SetWithNull()
        {
            var date1 = new DateTime(1966, 7, 14);
            var date2 = new DateTime(2018, 10, 5);
            var dateTimes = new DateTime?[] { date1, date2, null };
            var last = DateTimeUtilities.GetLastStartDate(dateTimes);
            Assert.AreEqual(date2, last);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetLastStartDate_SetWithoutNull()
        {
            var date1 = new DateTime(1966, 7, 14);
            var date2 = new DateTime(2018, 10, 5);
            var dateTimes = new DateTime?[] { date1, date2 };
            var last = DateTimeUtilities.GetLastStartDate(dateTimes);
            Assert.AreEqual(date2, last);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetFirstEndDate_EmptySet()
        {
            var dateTimes = new DateTime?[] { };
            var firstEndDate = DateTimeUtilities.GetFirstStartDate(dateTimes);
            Assert.IsNull(firstEndDate);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetFirstEndDate_OnlyNull()
        {
            var dateTimes = new DateTime?[] { null };
            var firstEndDate = DateTimeUtilities.GetFirstEndDate(dateTimes);
            Assert.IsNull(firstEndDate);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetFirstEndDate_SetWithNull()
        {
            var date1 = new DateTime(1966, 7, 14);
            var date2 = new DateTime(2018, 10, 5);
            var dateTimes = new DateTime?[] { date1, date2, null };
            var firstEndDate = DateTimeUtilities.GetFirstEndDate(dateTimes);
            Assert.AreEqual(date1, firstEndDate);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetFirstEndDate_SetWithoutNull()
        {
            var date1 = new DateTime(1966, 7, 14);
            var date2 = new DateTime(2018, 10, 5);
            var dateTimes = new DateTime?[] { date1, date2 };
            var firstEndDate = DateTimeUtilities.GetFirstEndDate(dateTimes);
            Assert.AreEqual(date1, firstEndDate);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetLastEndDate_EmptySet()
        {
            var dateTimes = new DateTime?[] { };
            var lastEndDate = DateTimeUtilities.GetLastEndDate(dateTimes);
            Assert.IsNull(lastEndDate);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetLastEndDate_OnlyNull()
        {
            var dateTimes = new DateTime?[] { null };
            var lastEndDate = DateTimeUtilities.GetLastEndDate(dateTimes);
            Assert.IsNull(lastEndDate);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetLastEndDate_SetWithNull()
        {
            var date1 = new DateTime(1966, 7, 14);
            var date2 = new DateTime(2018, 10, 5);
            var dateTimes = new DateTime?[] { date1, date2, null };
            var lastEndDate = DateTimeUtilities.GetLastEndDate(dateTimes);
            Assert.IsNull(lastEndDate);
        }

        [TestMethod]
        [TestCategory("DateTimeUtilitites")]
        [TestCategory("GatedCheckIn")]
        public void GetLastEndDate_SetWithoutNull()
        {
            var date1 = new DateTime(1966, 7, 14);
            var date2 = new DateTime(2018, 10, 5);
            var dateTimes = new DateTime?[] { date1, date2 };
            var lastEndDate = DateTimeUtilities.GetLastEndDate(dateTimes);
            Assert.AreEqual(date2, lastEndDate);
        }
    }
}
