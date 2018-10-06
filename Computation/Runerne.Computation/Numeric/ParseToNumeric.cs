using System;
using Runerne.Computation.String;

namespace Runerne.Computation.Numeric
{
    public class ParseToNumeric : INumericComputable
    {
        public double Value => Double.Parse(Input.Value);

        public IStringComputable Input { get; }

        public ParseToNumeric(IStringComputable input)
        {
            Input = input;
        }

        public ParseToNumeric(string input)
        {
            Input = new StringConstant(input);
        }
    }
}
