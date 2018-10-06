using Runerne.Computation.Numeric;

namespace Runerne.Computation.DateTime
{
    /// <summary>
    /// Returns the number of ticks for the timespan provided by the input node.
    /// </summary>
    public class TimeSpanTikcs : INumericComputable
    {
        /// <summary>
        /// Returns the ticks.
        /// </summary>
        public double Value => Input.Value.Ticks;

        /// <summary>
        /// The input node from which output the number of ticks are calculated.
        /// </summary>
        public ITimeSpanComputable Input { get; }

        /// <summary>
        /// Creates an instance of this timespan tick computable.
        /// </summary>
        /// <param name="input">The input node, which output will be used to determine the number of ticks.</param>
        public TimeSpanTikcs(ITimeSpanComputable input)
        {
            Input = input;
        }
    }
}
