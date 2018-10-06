using System;

namespace Runerne.Utilities
{
    /// <summary>
    /// General methods for working with math, such as numbers.
    /// </summary>
    public static class MathUtilities
    {
        /// <summary>
        /// Finds the floor value of the supplied source with respect to the step size. In this context floor means rounding down the source
        /// value to the nearest floor. The floor is quantified in steps.
        /// E.g.Floor to PI in 0.001 will be 3.141.
        /// E.g.Floor to 12345.678 in 100 is 12300.000.
        /// </summary>
        /// <param name="input">The value that will be floored.</param>
        /// <param name="stepSize">The quantification of the floors.</param>
        /// <returns>The nearest floor to the <see cref="input"/></returns>
        public static double Floor(double input, double stepSize)
        {
            return stepSize * (int)(input / stepSize);
        }

        /// <summary>
        /// Finds the floor value of the supplied source with respect to the step size. In this context floor means rounding down the source
        /// value to the nearest floor. The floor is quantified in steps.
        /// E.g.Floor to 12345 in 100 is 12300.
        /// </summary>
        /// <param name="input">The value that will be floored.</param>
        /// <param name="stepSize">The quantification of the floors.</param>
        /// <returns>The nearest floor to the <see cref="input"/></returns>
        public static int Floor(int input, int stepSize)
        {
            return (int)stepSize * (int)(input / stepSize);
        }
    }
}
