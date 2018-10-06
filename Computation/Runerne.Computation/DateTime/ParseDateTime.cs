using System.Globalization;
using Runerne.Computation.String;

namespace Runerne.Computation.DateTime
{
    public class ParseDateTime : IDateTimeComputable
    {
        public System.DateTime Value
        {
            get
            {
                return System.DateTime.ParseExact(DateTimeString.Value, Format.Value, CultureInfo.InvariantCulture);
            }
        }

        public IStringComputable DateTimeString { get; }
        public IStringComputable Format { get; }
        
        public ParseDateTime(IStringComputable dateTimeString, IStringComputable format)
        {
            DateTimeString = dateTimeString;
            Format = format;
        }

    }
}
