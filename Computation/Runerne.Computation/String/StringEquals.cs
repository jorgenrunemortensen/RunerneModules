using System.Collections.Generic;
using Runerne.Computation.Logic;

namespace Runerne.Computation.String
{
    /// <summary>
    /// This computable compares a set of strings.
    /// </summary>
    public class StringEquals : IBooleanComputable
    {
        /// <summary>
        /// The result of the comparison. If all the provided inputs represent the same string, then Value is true; otherwise false.
        /// </summary>
        public bool Value
        {
            get
            {
                string value = null;
                bool first = true;
                foreach (var input in Inputs)
                {
                    if (first)
                    {
                        value = input.Value;
                        first = false;
                        continue;
                    }

                    if (!(value == input.Value))
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// The computables, which output will be compared by this computable.
        /// </summary>
        public IEnumerable<IStringComputable> Inputs { get; }

        /// <summary>
        /// Creates an instance of this computable with the input nodes, which values will be compared as strings.
        /// </summary>
        /// <param name="inputs">The input nodes.</param>
        public StringEquals(params IStringComputable[] inputs)
        {
            Inputs = inputs;
        }

        public StringEquals(IEnumerable<IStringComputable> inputs)
        {
            Inputs = inputs;
        }

        public StringEquals(IStringComputable input1, IStringComputable input2)
        {
            Inputs = new List<IStringComputable>() { input1, input2 };
        }
    }
}
