namespace Runerne.Computation.Numeric
{
    /// <summary>
    /// A numeric computable that negates (multiplies by -1) the input into its output.
    /// </summary>
    public class Negate : INumericComputable
    {
        /// <summary>
        /// The negated output. 
        /// </summary>
        public double Value => -Input.Value;

        /// <summary>
        /// The input that will be negated by the computable.
        /// </summary>
        public INumericComputable Input { get; }

        /// <summary>
        /// Creates an instance of the negator with the required input.
        /// </summary>
        /// <param name="input">The input computable which output will be converted.</param>
        public Negate(INumericComputable input)
        {
            Input = input;
        }
    }
}
