using System.Collections.Generic;
using Runerne.Computation.Logic;

namespace Runerne.Computation.Numeric
{
    /// <summary>
    /// Compares a set of numeric values to each other. If all equals the computable returns true; otherwise false.
    /// </summary>
    public class NumericEquals : IBooleanComputable
    {
        /// <summary>
        /// Returns the result of the compare.
        /// </summary>
        public bool Value
        {
            get
            {
                double? value = null;
                foreach (var input in Inputs)
                {
                    if (value == null)
                        value = input.Value;
                    else
                    {
                        if (input.Value != value)
                            return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// The inputs which are compared.
        /// </summary>
        public IEnumerable<INumericComputable> Inputs { get; }

        /// <summary>
        /// Creates an instance of the computable with the inputs, which are compared.
        /// </summary>
        /// <param name="inputs"></param>
        public NumericEquals(params INumericComputable[] inputs)
        {
            Inputs = inputs;
        }
        public NumericEquals(IEnumerable<INumericComputable> inputs)
        {
            Inputs = inputs;
        }
    }
}
