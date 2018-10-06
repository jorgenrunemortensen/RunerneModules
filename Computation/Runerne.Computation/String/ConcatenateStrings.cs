using System.Collections.Generic;
using System.Text;

namespace Runerne.Computation.String
{
    /// <summary>
    /// A string node that can produce a string, which is concatenated by the values provided by input nodes (<see cref="Inputs"/>).
    /// </summary>
    public class ConcatenateStrings : IStringComputable
    {
        /// <summary>
        /// Returns the string value made by concatenating the strings provided by the input nodes (<see cref="Inputs"/>).
        /// </summary>
        public string Value
        {
            get
            {
                var stringBuilder = new StringBuilder();
                foreach (var input in Inputs)
                    stringBuilder.Append(input.Value);
                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// This string nodes, which values are concatenated into the produced string.
        /// </summary>
        public IEnumerable<IStringComputable> Inputs { get; }

        /// <summary>
        /// Creates a string node, which provides the concatenated string of all input nodes. 
        /// </summary>
        /// <param name="inputs">The input nodes, which values will be concatenated.</param>
        public ConcatenateStrings(params IStringComputable[] inputs)
        {
            Inputs = inputs;
        }
    }
}
