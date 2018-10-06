namespace Runerne.Computation.Logic
{
    /// <summary>
    /// Implements an if-then-else computable. The computable returns wither the value from a then-computable or from an else-computable
    /// depending on the boolean output from a condition-computable.
    /// </summary>
    /// <typeparam name="T">The returns type of the values of the then- and else-computable.</typeparam>
    public class If<T> : IComputable<T>
    {
        /// <summary>
        /// The value returned by the instance. If the condition-computable returns a boolean true then the value from the then-computable is
        /// returned; otherwise the value from the else-computable is returned.
        /// </summary>
        public T Value => Condition.Value ? ThenComputable.Value : ElseComputable.Value;

        /// <summary>
        /// The condition-computable.
        /// </summary>
        public IBooleanComputable Condition { get; }

        /// <summary>
        /// The then-computable.
        /// </summary>
        public IComputable<T> ThenComputable { get; }

        /// <summary>
        /// The else-computable.
        /// </summary>
        public IComputable<T> ElseComputable { get; }

        /// <summary>
        /// Creates an instance of the if-then-else-computable with the condition-, then- and else-computable.
        /// </summary>
        /// <param name="condition">The condition-computable.</param>
        /// <param name="thenComputable">The then-computable.</param>
        /// <param name="elseComputable">The else-computable.</param>
        public If(IBooleanComputable condition, IComputable<T> thenComputable, IComputable<T> elseComputable)
        {
            Condition = condition;
            ThenComputable = thenComputable;
            ElseComputable = elseComputable;
        }
    }
}
