namespace Runerne.Computation.Logic
{
    /// <summary>
    /// A boolean node, which is always false. The node is a singleton, which is available through <see cref="Instance"/>.
    /// </summary>
    public sealed class BooleanFalse : IBooleanComputable
    {
        /// <summary>
        /// Returns always false.
        /// </summary>
        public bool Value => false;

        /// <summary>
        /// Returns the singleton of this class.
        /// </summary>
        public static BooleanFalse Instance => _instance ?? (_instance = new BooleanFalse());

        private static BooleanFalse _instance;

        private BooleanFalse()
        {
        }
    }
}
