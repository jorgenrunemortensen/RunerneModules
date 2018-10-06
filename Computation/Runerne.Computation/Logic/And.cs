using System.Collections.Generic;
using System.Linq;

namespace Runerne.Computation.Logic
{
    /// <summary>
    /// A Boolean node that returns the logical AND of all input nodes.
    /// </summary>
    public class And : IBooleanComputable
    {
        /// <summary>
        /// Returns the boolean AND-value of all input nodes.
        /// </summary>
        public bool Value
        {
            get
            {
                return Inputs.All(input => input.Value);
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
        public And(params IBooleanComputable[] inputs)
        {
            Inputs = inputs;
        }

        public And(IEnumerable<IBooleanComputable> inputs)
        {
            Inputs = inputs;
        }
    }
}
