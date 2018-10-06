using System;
using System.Collections.Generic;
using System.Linq;

namespace Runerne.Computation.DateTime
{
    /// <summary>
    /// A time span node that can add time spans from other timespan nodes.
    /// </summary>
    public class AddTimeSpans : ITimeSpanComputable
    {
        /// <summary>
        /// Returns the sum of all the timespans provided by the specified timespan nodes (Inputs).
        /// </summary>
        public TimeSpan Value
        {
            get
            {
                return Inputs.Aggregate(TimeSpan.Zero, (current, timeSpan) => current + timeSpan.Value);
            }
        }

        /// <summary>
        /// The timespan nodes, which supplies the timespans, which are added.
        /// </summary>
        public IEnumerable<ITimeSpanComputable> Inputs { get; }

        /// <summary>
        /// Creates a timespan node, which can add the timespans of an arbitrary number of input nodes.
        /// </summary>
        /// <param name="inputs">The timespan nodes, which supplies the values, which are added.</param>
        public AddTimeSpans(params ITimeSpanComputable[] inputs)
        {
            Inputs = inputs;
        }
    }
}
