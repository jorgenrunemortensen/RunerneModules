using System;
using System.Linq;

namespace Runerne.Utilities
{
    /// <summary>
    /// General methods for working with dates and time, such as numbers.
    /// </summary>
    public static class DateTimeUtilities
    {
        /// <summary>
        /// Floors the provided date and time to highest step value, which are the same or lower. Flooring is defined as taking a value and quantifying it into
        /// one of the defined step, where the selected step is the one closest, below the value. E.g. if the value PI is given and it should be quantified
        /// into eights (1/8) the steps will be all numbers, which can be divided by 8 without any remainders, hence the floor of PI (3.141) will be 3.125.
        /// The same goes for flooring dates and times, which is what this function supports; however, the you must specify what part of the date time you
        /// would like to quantify, and by which value.
        /// E.g.given the date time 1966-07-14 13:46:15:316. Let’s say we want to quantify it into steps of 10 minutes.This will give the value
        /// 1966-07-1 13:40:00:000, hence this is the closest value that are less or equal to the original date and time and which can be divided by 10 minutes
        /// without any remainder.
        /// To utilize the method, you must therefore provide the source date time value, specify what part of the date time you would like to define the
        /// quantification for, and the step size for that part.
        /// </summary>
        /// <param name="source">The date time to be floored.</param>
        /// <param name="part">The part of the date time, that the <see cref="stepSize"/> is defined on.</param>
        /// <param name="stepSize">The step size of the defined <see cref="part"/>.</param>
        /// <returns>The floored value of <see cref="source"/>.</returns>
        public static DateTime FloorDateTime(DateTime source, DateTimePart part, int stepSize = 1)
        {
            switch (part)
            {
                case DateTimePart.Milliseconds:
                    return new DateTime(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, MathUtilities.Floor(source.Millisecond, stepSize));

                case DateTimePart.Seconds:
                    return new DateTime(source.Year, source.Month, source.Day, source.Hour, source.Minute, MathUtilities.Floor(source.Second, stepSize), 0);

                case DateTimePart.Minutes:
                    return new DateTime(source.Year, source.Month, source.Day, source.Hour, MathUtilities.Floor(source.Minute, stepSize), 0, 0);

                case DateTimePart.Hours:
                    return new DateTime(source.Year, source.Month, source.Day, MathUtilities.Floor(source.Hour, stepSize), 0, 0, 0);

                case DateTimePart.Days:
                    return new DateTime(source.Year, source.Month, 1 + MathUtilities.Floor(source.Day - 1, stepSize), 0, 0, 0, 0);

                case DateTimePart.Months:
                    return new DateTime(source.Year, 1 + MathUtilities.Floor(source.Month - 1, stepSize), 1, 0, 0, 0);

                case DateTimePart.Years:
                    return new DateTime(MathUtilities.Floor(source.Year, stepSize), 1, 1, 0, 0, 0);

                case DateTimePart.Weeks:
                    throw new NotSupportedException($"FloorDateTime method does not support flooring of DateTime based on DateTimePart.Weeks");

                default:
                    var partOptions = string.Join(", ", Enum.GetNames(typeof(DateTimePart)));
                    throw new ArgumentException($"The provided part must be a member of the enumeration DateTimePart. Options are {partOptions}.");
            }
        }

        /// <summary>
        /// Determines the first (earliest) start date from a set of unsorted start dates. If a null value occurs, then null is determined 
        /// as the earliest start date. This is based on the rule, that a null value indicates that the entity has always been running.
        /// </summary>
        /// <param name="startDates">The start dates from which the first start date is derived.</param>
        /// <returns>The first start date and null if one or more of the entities always has been started.</returns>
        public static DateTime? GetFirstStartDate(params DateTime?[] startDates)
        {
            if (startDates.Any(o => o == null))
            {
                return null;
            }

            return startDates.OrderBy(o => o).FirstOrDefault();
        }

        /// <summary>
        /// Determines the last (latest) start date from a set of unsorted start dates. If a null value occurs, then null is determined as 
        /// the earliest start date. This is based on the rule, that a null value indicates that the entity has always been running.
        /// </summary>
        /// <param name="startDates">The start dates from which the last start date is derived.</param>
        /// <returns>The last start date and null if no or only null-values are given.</returns>
        public static DateTime? GetLastStartDate(params DateTime?[] startDates)
        {
            return startDates.Where(o => o != null).OrderBy(o => o).LastOrDefault();
        }

        /// <summary>
        /// Determines the first (earliest) end date from a set of unsorted end dates. If a null value occurs, then null is determined as 
        /// the latest start date. This is based on the rule, that a null value indicates that the entity is never to be terminated.
        /// </summary>
        /// <param name="endDates">The end dates from which the first end date is derived.</param>
        /// <returns>The first end date and null if no or only null-values are given.</returns>
        public static DateTime? GetFirstEndDate(params DateTime?[] endDates)
        {

            return endDates.Where(o => o != null).OrderBy(o => o).FirstOrDefault();
        }

        /// <summary>
        /// Determines the last (latest) end date from a set of unsorted end dates. If a null value occurs, then null is determined as the 
        /// latest end date. This is based on the rule, that a null value indicates that the entity is never to be terminated.
        /// </summary>
        /// <param name="endDates">The end dates from which the first end date is derived.</param>
        /// <returns>The first end date and null if one or more of the entities always has been ended.</returns>
        public static DateTime? GetLastEndDate(params DateTime?[] endDates)
        {
            if (endDates.Any(o => o == null))
            {
                return null;
            }

            return endDates.OrderBy(o => o).LastOrDefault();
        }
    }
}
