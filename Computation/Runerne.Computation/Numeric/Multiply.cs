using System.Collections.Generic;
using System.Linq;

namespace Runerne.Computation.Numeric
{
    /// <summary>
    /// Creates a numeric computable, which is capable of mulitplying outputs from an arbitrary number of numeric computables.
    /// </summary>
    public class Multiply : INumericComputable
    {
        /// <summary>
        /// Returns the value given by mulitiplying the values from all the provided input nodes.
        /// </summary>
        public double Value
        {
            get { return Inputs.Aggregate(1.0, (current, input) => current*input.Value); }
        }

        /// <summary>
        /// The inputs, which values are multiplied.
        /// </summary>
        public IEnumerable<INumericComputable> Inputs { get; }

        /// <summary>
        /// Creates an instance of the multiplier with the inputs.
        /// </summary>
        /// <param name="inputs">The numeric input computables.</param>
        public Multiply(params INumericComputable[] inputs)
        {
            Inputs = inputs;
        }
    }
}
