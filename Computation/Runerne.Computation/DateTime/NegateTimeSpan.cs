using System;

namespace Runerne.Computation.DateTime
{
    /// <summary>
    /// Negates a timespan.
    /// </summary>
    public class NegateTimeSpan : ITimeSpanComputable
    {
        /// <summary>
        /// Returns the negative value of the timespan input node.
        /// </summary>
        public TimeSpan Value => -Input.Value;

        /// <summary>
        /// The timespan input node.
        /// </summary>
        public ITimeSpanComputable Input { get; }

        /// <summary>
        /// Creates an instance of the node with the input, which timespan will be negated.
        /// </summary>
        /// <param name="input"></param>
        public NegateTimeSpan(ITimeSpanComputable input)
        {
            Input = input;
        }
    }
}
