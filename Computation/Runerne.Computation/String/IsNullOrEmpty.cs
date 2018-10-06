using Runerne.Computation.Logic;

namespace Runerne.Computation.String
{
    public class IsNullOrEmpty : IBooleanComputable
    {
        public bool Value
        {
            get
            {
                return Input == null || string.IsNullOrEmpty(Input.Value);
            }
        }

        public IStringComputable Input { get; }

        public IsNullOrEmpty(IStringComputable input)
        {
            Input = input;
        }

        public IsNullOrEmpty(string input)
        {
            Input = new StringConstant(input);
        }
    }
}
