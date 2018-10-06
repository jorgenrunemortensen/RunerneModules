using System;

namespace Runerne.Computation.DateTime
{
    /// <summary>
    /// Specifies that a class implementing this interface will be capable of returning a timespan value as output.
    /// </summary>
    public interface ITimeSpanComputable : IComputable<TimeSpan>
    {
    }
}
