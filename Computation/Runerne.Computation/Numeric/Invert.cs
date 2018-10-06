namespace Runerne.Computation.Numeric
{
    /// <summary>
    /// A numeric computable that inverts (1/x) the input into its output.
    /// </summary>
    public class Invert : INumericComputable
    {
        /// <summary>
        /// The inverted output. If input is zero, then an exception will be thrown.
        /// </summary>
        public double Value => 1 / Input.Value;
        
        /// <summary>
        /// The input that will be inverted by the computable.
        /// </summary>
        public INumericComputable Input { get; }

        /// <summary>
        /// Creates an instance of the inverter with the required input.
        /// </summary>
        /// <param name="input">The input computable which output will be converted.</param>
        public Invert(INumericComputable input)
        {
            Input = input;
        }
    }
}
