using System.Collections.Generic;
using System.Linq;

namespace Runerne.Computation.DateTime
{
    /// <summary>
    /// A date time node that can add time spans to a date time node.
    /// </summary>
    public class AddToDateTime : IDateTimeComputable
    {
        /// <summary>
        /// Returns the datetime that are displaced by the sum of all the timespans provided by the specified timespan nodes (Inputs).
        /// </summary>
        public System.DateTime Value
        {
            get
            {
                return TimeSpans.Aggregate(Offset.Value, (current, timeSpan) => current + timeSpan.Value);
            }
        }

        /// <summary>
        /// The date time node that will be used as the offset for the displacement.
        /// </summary>
        public IDateTimeComputable Offset { get; }
        /// <summary>
        /// The timespan nodes, which supplies the timespans, which are added to the <see cref="Offset"/>.
        /// </summary>
        public IEnumerable<ITimeSpanComputable> TimeSpans { get; }

        /// <summary>
        /// Creates a date time node, which provides the displaced date time value as the <see cref="offset"/> date time that is displaced by the <see cref="TimeSpans"/>.
        /// </summary>
        /// <param name="offset">The offset date time, which will be displaced by the node.</param>
        /// <param name="timeSpans">The timespan nodes, which values will displace the calculated date time.</param>
        public AddToDateTime(IDateTimeComputable offset, params ITimeSpanComputable[] timeSpans)
        {
            Offset = offset;
            TimeSpans = timeSpans;
        }
    }
}
