using System;

namespace Runerne.Computation.DateTime
{
    /// <summary>
    /// A constant timespan.
    /// </summary>
    public class TimeSpanConstant : ITimeSpanComputable
    {
        /// <summary>
        /// The constant timespand provided by this computable.
        /// </summary>
        public TimeSpan Value => Input;

        /// <summary>
        /// The constant timespand provided by this computable.
        /// </summary>
        public TimeSpan Input { get; }

        /// <summary>
        /// Creates an instance of this constant timespand computable with the given timespan.
        /// </summary>
        /// <param name="input">The timespand that will be constant for this instance.</param>
        public TimeSpanConstant(TimeSpan input)
        {
            Input = input;
        }
    }
}
