using Runerne.Computation.Numeric;

namespace Runerne.Computation.DateTime
{
    /// <summary>
    /// A numeric node, which returns the ticks of a date time input node.
    /// </summary>
    public class DateTimeTicks : INumericComputable
    {
        /// <summary>
        /// Returns the ticks of the provided input node.
        /// </summary>
        public double Value => Input.Value.Ticks;

        /// <summary>
        /// The input node.
        /// </summary>
        public IDateTimeComputable Input { get; }

        /// <summary>
        /// Creates an instance of this type with the input node.
        /// </summary>
        /// <param name="input">The input node.</param>
        public DateTimeTicks(IDateTimeComputable input)
        {
            Input = input;
        }
    }
}
