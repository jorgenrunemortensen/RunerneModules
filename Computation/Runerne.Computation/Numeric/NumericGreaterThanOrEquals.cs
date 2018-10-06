using Runerne.Computation.Logic;

namespace Runerne.Computation.Numeric
{
    /// <summary>
    /// Compares the values of two numeric computables (Left and Right).
    /// </summary>
    public class NumericGreaterThanOrEquals : IBooleanComputable
    {
        /// <summary>
        /// Returns true if Left >= Right; otherwise false.
        /// </summary>
        public bool Value => Left.Value >= Right.Value;

        /// <summary>
        /// The left numeric computable.
        /// </summary>
        public INumericComputable Left { get; }

        /// <summary>
        /// The right numeric computable.
        /// </summary>
        public INumericComputable Right { get; }

        /// <summary>
        /// Creates an instance of this numeric comperator.
        /// </summary>
        /// <param name="left">The left input nodes in the compare.</param>
        /// <param name="right">The rightinput nodes in the compare.</param>
        public NumericGreaterThanOrEquals(INumericComputable left, INumericComputable right)
        {
            Left = left;
            Right = right;
        }
        public NumericGreaterThanOrEquals(INumericComputable left, double right)
        {
            Left = left;
            Right = new NumericConstant(right);
        }
        public NumericGreaterThanOrEquals(double left, INumericComputable right)
        {
            Left = new NumericConstant(left);
            Right = right;
        }
    }
}
