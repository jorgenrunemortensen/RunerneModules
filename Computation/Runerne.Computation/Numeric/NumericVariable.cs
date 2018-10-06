using System;

namespace Runerne.Computation.Numeric
{
    /// <summary>
    /// A numeric computable which value can be set and thereby controlled from code.
    /// </summary>
    public class NumericVariable : INumericComputable
    {
        /// <summary>
        /// The numeric value that can be set and thereby controlled from code.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Creates an instance of this variable with the initial value.
        /// </summary>
        /// <param name="initialValue">This is the initial value that the variable will be given during construction.</param>
        public NumericVariable(double initialValue)
        {
            Value = initialValue;
        }
    }
}
