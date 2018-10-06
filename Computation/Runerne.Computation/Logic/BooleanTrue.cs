namespace Runerne.Computation.Logic
{
    /// <summary>
    /// A boolean node, which is always true. The node is a singleton, which is available through <see cref="Instance"/>.
    /// </summary>
    public sealed class BooleanTrue : IBooleanComputable
    {
        /// <summary>
        /// Returns always true.
        /// </summary>
        public bool Value => true;

        /// <summary>
        /// Returns the singleton of this class.
        /// </summary>
        public static BooleanTrue Instance => _instance ?? (_instance = new BooleanTrue());

        private static BooleanTrue _instance;

        private BooleanTrue()
        {
        }
    }
}
