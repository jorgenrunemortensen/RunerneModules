using System.Collections.Generic;
using System.Linq;

namespace Runerne.Computation.Logic
{
    /// <summary>
    /// A Boolean node that returns the logical OR of all input nodes.
    /// </summary>
    public class Or : IBooleanComputable
    {
        /// <summary>
        /// Returns the boolean OR-value of all input nodes.
        /// </summary>
        public bool Value
        {
            get
            {
                return Inputs.Any(input => input.Value);
            }
        }

        /// <summary>
        /// The input nodes.
        /// </summary>
        public IEnumerable<IBooleanComputable> Inputs { get; }

        /// <summary>
        /// Creates a Boolean node, which provides the logical boolean value of all <see cref="Inputs"/>.
        /// </summary>
        /// <param name="inputs"></param>
        public Or(params IBooleanComputable[] inputs)
        {
            Inputs = inputs;
        }

        public Or(IEnumerable<IBooleanComputable> inputs)
        {
            Inputs = inputs;
        }
    }
}
