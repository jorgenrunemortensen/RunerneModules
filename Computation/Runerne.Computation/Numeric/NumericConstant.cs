namespace Runerne.Computation.Numeric
{
    /// <summary>
    /// This computable returns a constant numeric value.
    /// </summary>
    public class NumericConstant : INumericComputable
    {
        /// <summary>
        /// The constant value provided to the constructor.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Creates an instance of the numeric constant from the specified value.
        /// </summary>
        /// <param name="value">The string that always will be returned by this instance.</param>
        public NumericConstant(double value)
        {
            Value = value;
        }
    }
}
