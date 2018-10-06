namespace Runerne.Computation.Logic
{
    /// <summary>
    /// Applies a boolean NOT to the boolean input node.
    /// </summary>
    public class Not : IBooleanComputable
    {
        /// <summary>
        /// Returns the opposite boolean value to the value provided by the input node.
        /// </summary>
        public bool Value => !Input.Value;

        /// <summary>
        /// The input which output value is inverted.
        /// </summary>
        public IBooleanComputable Input { get; }

        /// <summary>
        /// Creates an instance of the inverter with the required input node.
        /// </summary>
        /// <param name="input">The input node.</param>
        public Not(IBooleanComputable input)
        {
            Input = input;
        }
    }
}
