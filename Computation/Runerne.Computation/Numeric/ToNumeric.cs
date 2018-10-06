using System;

namespace Runerne.Computation.Numeric
{
    public class ToNumeric : INumericComputable
    {
        private object _input;

        public double Value => Convert.ToDouble(_input);

        public ToNumeric(object input)
        {
            _input = input;
        }
    }
}
