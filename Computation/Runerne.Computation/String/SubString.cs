using System;
using Runerne.Computation.Numeric;

namespace Runerne.Computation.String
{
    /// <summary>
    /// This computable works like the SubString-operation for the class String. This computable works like the SubString-operation for the
    /// class String, but where the source string, the start index and the length are taken from other computables.
    /// </summary>
    public class SubString : IStringComputable
    {
        /// <summary>
        /// The substring of the string provided by the input.
        /// </summary>
        public string Value
        {
            get
            {
                var s = Source.Value;
                var startIndex = (int)StartIndex.Value;
                var length = (int) Length.Value;
                if (startIndex >= s.Length)
                    return string.Empty;

                var appliedLength = Math.Min(length, s.Length - startIndex);
                return s.Substring(startIndex, appliedLength);
            }
        }

        /// <summary>
        /// The source of the string, which sub string is determined.
        /// </summary>
        public IStringComputable Source { get; }

        /// <summary>
        /// The input that provides the zero-based starting character position of a substring in this instance.
        /// </summary>
        public INumericComputable StartIndex { get; }

        /// <summary>
        /// The input providing the number of characters in the substring.
        /// </summary>
        public INumericComputable Length { get; }

        /// <summary>
        /// Creates an instance of this substring computable with a computable for the source, the start index and the length.
        /// </summary>
        /// <param name="source">The computable that provides the string from which the substring is retrieved.</param>
        /// <param name="startIndex">The computable that provides the start index for the operation.</param>
        /// <param name="length">The computable that provides the length for the operation.</param>
        public SubString(IStringComputable source, INumericComputable startIndex, INumericComputable length)
        {
            Source = source;
            StartIndex = startIndex;
            Length = length;
        }

        /// <summary>
        /// Creates an instance of this substring computable with a computable for the source and the start index but the length as a fixed
        /// number.
        /// </summary>
        /// <param name="source">The computable that provides the string from which the substring is retrieved.</param>
        /// <param name="startIndex">The computable that provides the start index for the operation.</param>
        /// <param name="length">The length as a fixed number.</param>
        public SubString(IStringComputable source, INumericComputable startIndex, int length)
            : this(source, startIndex, new NumericConstant(length))
        {
        }

        /// <summary>
        /// Creates an instance of this substring computable with a computable for the source and the length but with the start index as a 
        /// fixed number.
        /// </summary>
        /// <param name="source">The computable that provides the string from which the substring is retrieved.</param>
        /// <param name="startIndex">The start index as a fixed number.</param>
        /// <param name="length">The computable that provides the length for the operation.</param>
        public SubString(IStringComputable source, int startIndex, INumericComputable length)
            : this(source, new NumericConstant(startIndex), length)
        {
        }

        /// <summary>
        /// Creates an instance of this substring computable with a computable for the source but with the start index and the length as
        /// fixed numbers.
        /// </summary>
        /// <param name="source">The computable that provides the string from which the substring is retrieved.</param>
        /// <param name="startIndex">The start index as a fixed number.</param>
        /// <param name="length">The length as a fixed number.</param>
        public SubString(IStringComputable source, int startIndex, int length)
            : this(source, new NumericConstant(startIndex), new NumericConstant(length))
        {
        }
    }
}
