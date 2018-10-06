using Runerne.Computation.Numeric;

namespace Runerne.Computation.String
{
    /// <summary>
    /// Derives the zero-based index of the first occurrence of the specified string (<see cref="SubString"/>) in <see cref="Source"/>. The search starts at a
    /// specified position (<see cref="StartIndex"/>).
    /// </summary>
    public class IndexOfString : INumericComputable
    {
        /// <summary>
        /// The position of the substring string in the specified string. If no such sub-string is found, then -1 is returned.
        /// </summary>
        public double Value
        {
            get
            {
                var source = Source.Value;
                var subString = SubString.Value;
                var startIndex = (int)StartIndex.Value;
                return source.IndexOf(subString, startIndex);
            }
        }

        /// <summary>
        /// The computable that supplies the string from which output is searched.
        /// </summary>
        public IStringComputable Source { get; }

        /// <summary>
        /// The computable that supplies the substring that is searched for.
        /// </summary>
        public IStringComputable SubString { get; }

        /// <summary>
        /// The computable that supplies the start index of the search.
        /// </summary>
        public INumericComputable StartIndex { get; }

        /// <summary>
        /// Creates an instance of this computable with the required computables.
        /// </summary>
        /// <param name="source">The source computable.</param>
        /// <param name="subString">The sub-string computable.</param>
        /// <param name="startIndex">The start index computable.</param>
        public IndexOfString(IStringComputable source, IStringComputable subString, INumericComputable startIndex)
        {
            Source = source;
            SubString = subString;
            StartIndex = startIndex;
        }

        /// <summary>
        /// Creates an instance of this computable with the source and sub-string computable. The start index will always be 0.
        /// </summary>
        /// <param name="source">The source computable.</param>
        /// <param name="subString">The sub-string computable.</param>
        public IndexOfString(IStringComputable source, IStringComputable subString) : this(source, subString, new NumericConstant(0))
        {
        }

        /// <summary>
        /// Creates an instance of this computable with the source computable, a fixed sub-string and a start index computable.
        /// </summary>
        /// <param name="source">The source computable.</param>
        /// <param name="subString">The fixed sub-string.</param>
        /// <param name="startIndex">The start index computable.</param>
        public IndexOfString(IStringComputable source, string subString, INumericComputable startIndex) : this(source, new StringConstant(subString), startIndex)
        {
        }

        /// <summary>
        /// Creates an instance of this computable with the source computable and a fixed sub-string. The start index will always be 0.
        /// </summary>
        /// <param name="source">The source computable.</param>
        /// <param name="subString">The fixed sub-string.</param>
        public IndexOfString(IStringComputable source, string subString) : this(source, new StringConstant(subString), new NumericConstant(0))
        {
        }

        /// <summary>
        /// Creates an instance of this computable with the source computable, a fixed sub-string and a fixed start index.
        /// </summary>
        /// <param name="source">The source computable.</param>
        /// <param name="subString">The fixed sub-string.</param>
        /// <param name="startIndex">The fixed start index.</param>
        public IndexOfString(IStringComputable source, string subString, int startIndex) : this(source, new StringConstant(subString), new NumericConstant(startIndex))
        {
        }
    }
}
