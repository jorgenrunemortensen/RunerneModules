namespace Runerne.Computation.Logic
{
    /// <summary>
    /// Determines the exclusive or value of two boolean input nodes.
    /// </summary>
    public class XOr : IBooleanComputable
    {
        /// <summary>
        /// Returns the XOR value of input node A and B.
        /// </summary>
        public bool Value => InputA.Value != InputB.Value;

        /// <summary>
        /// The A input node.
        /// </summary>
        public IBooleanComputable InputA { get; }

        /// <summary>
        /// The B input node.
        /// </summary>
        public IBooleanComputable InputB { get; }

        /// <summary>
        /// Creates an instance of the XOr computable with two boolean computables as inputs.
        /// </summary>
        /// <param name="inputA">The input computable A.</param>
        /// <param name="inputB">The input computable B.</param>
        public XOr(IBooleanComputable inputA, IBooleanComputable inputB)
        {
            InputA = inputA;
            InputB = inputB;
        }
    }
}
