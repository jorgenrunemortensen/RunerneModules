using System;
using System.Globalization;
using Runerne.Computation.Logic;
using Runerne.Computation.String;

namespace Runerne.Computation.DateTime
{
    public class IsValidDateTime : IBooleanComputable
    {
        public bool Value
        {
            get
            {
                try
                {
                    System.DateTime.ParseExact(DateTimeString.Value, Format.Value, CultureInfo.InvariantCulture);
                    return true;
                }
                catch(Exception)
                {
                    return false;
                }
            }
        }

        public IStringComputable DateTimeString { get; }
        public IStringComputable Format { get; }

        public IsValidDateTime(IStringComputable dateTimeString, IStringComputable format)
        {
            DateTimeString = dateTimeString;
            Format = format;
        }
        public IsValidDateTime(IStringComputable dateTimeString, string format) :
            this(dateTimeString, new StringConstant(format))
        { }
    }
}
