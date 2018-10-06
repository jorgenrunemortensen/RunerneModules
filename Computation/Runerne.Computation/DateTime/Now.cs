namespace Runerne.Computation.DateTime
{
    /// <summary>
    /// This node returns the current date and time as a date time computable.
    /// </summary>
    public class Now : IDateTimeComputable
    {
        /// <summary>
        /// The current date and time.
        /// </summary>
        public System.DateTime Value => System.DateTime.Now;
    }
}
