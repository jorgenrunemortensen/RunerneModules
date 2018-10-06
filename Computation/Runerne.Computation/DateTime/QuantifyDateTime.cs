using Runerne.Computation.Numeric;
using Runerne.Utilities;

namespace Runerne.Computation.DateTime
{
    /// <summary>
    /// A note that can quantify a date time value into the nearest date time that matches the specified steps
    /// (<see cref="Runerne.Utilities.DateTimeUtilities.FloorDateTime"/>).
    /// </summary>
    public class QuantifyDateTime : IDateTimeComputable
    {
        /// <summary>
        /// The quantified date time.
        /// </summary>
        public System.DateTime Value
        {
            get
            {
                var source = Input.Value;
                var stepSize = (int) StepSize.Value;
                return DateTimeUtilities.FloorDateTime(source, Part, stepSize);
            }
        }

        /// <summary>
        /// The input node, which date time value is quantified.
        /// </summary>
        public IDateTimeComputable Input { get; }

        /// <summary>
        /// The part by which the quantification takes place.
        /// </summary>
        public DateTimePart Part { get; }

        /// <summary>
        /// The step size of the quantifications.
        /// </summary>
        public INumericComputable StepSize { get; }

        /// <summary>
        /// Creates an instance of this computable quantification.
        /// </summary>
        /// <param name="input">The node, which value will be quantified.</param>
        /// <param name="part">The part, by which the quantification takes place.</param>
        /// <param name="stepSize">The numberic computable that supplies the step size.
        public QuantifyDateTime(IDateTimeComputable input, DateTimePart part, INumericComputable stepSize)
        {
            Input = input;
            Part = part;
            StepSize = stepSize;
        }

        /// <summary>
        /// Creates an instance of this computable quantification.
        /// </summary>
        /// <param name="input">The node, which value will be quantified.</param>
        /// <param name="part">The part, by which the quantification takes place.</param>
        /// <param name="stepSize">The steps by which the quantification takes place.</param></param>
        public QuantifyDateTime(IDateTimeComputable input, DateTimePart part, int stepSize) : this(input, part, new NumericConstant(stepSize))
        {
        }
    }
}
