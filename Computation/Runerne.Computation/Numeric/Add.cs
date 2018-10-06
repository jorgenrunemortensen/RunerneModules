using System.Collections.Generic;
using System.Linq;

namespace Runerne.Computation.Numeric
{
    /// <summary>
    /// A numeric node that can add numeric values from other numeric nodes.
    /// </summary>
    public class Add : INumericComputable
    {
        /// <summary>
        /// Returns the sum of all the values provided by the specified numeric nodes (Inputs).
        /// </summary>
        public double Value {
            get
            {
                return Inputs.Sum(input => input.Value);
            }
        }

        /// <summary>
        /// Returns the input nodes.
        /// </summary>
        public IEnumerable<INumericComputable>  Inputs { get; }

        /// <summary>
        /// Creates a numeric node, which can add the numeric values of an arbitrary number of input nodes.
        /// </summary>
        /// <param name="inputs">The numeric nodes, which supplies the values, which are added.</param>
        public Add(params INumericComputable[] inputs)
        {
            Inputs = inputs;
        }
    }
}
