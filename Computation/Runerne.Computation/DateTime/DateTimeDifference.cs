using System;

namespace Runerne.Computation.DateTime
{
    /// <summary>
    /// A node that returns the timespan between two date time nodes (<see cref="InputFirst"/> and <see cref="InputLast"/>).
    /// </summary>
    public class DateTimeDifference : ITimeSpanComputable
    {
        /// <summary>
        /// Returns the timespan between the two date time nodes.
        /// </summary>
        public TimeSpan Value => InputLast.Value - InputFirst.Value;

        /// <summary>
        /// The first date time node.
        /// </summary>
        public IDateTimeComputable InputFirst { get; }

        /// <summary>
        /// The second date time node.
        /// </summary>
        public IDateTimeComputable InputLast { get; }

        /// <summary>
        /// Creates an instance of this type with the two input date time nodes.
        /// </summary>
        /// <param name="inputFirst">The first date time node.</param>
        /// <param name="inputLast">The second date time node.</param>
        public DateTimeDifference(IDateTimeComputable inputFirst, IDateTimeComputable inputLast)
        {
            InputFirst = inputFirst;
            InputLast = inputLast;
        }
    }
}
