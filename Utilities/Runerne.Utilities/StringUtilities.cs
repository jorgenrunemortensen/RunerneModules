using System;
using System.IO;
using System.Text;

namespace Runerne.Utilities
{
    /// <summary>
    /// Common string utilities.
    /// </summary>
    public static class StringUtilities
    {
        /// <summary>
        /// Inserts a lead text in front of each line in the text.
        /// </summary>
        /// <param name="leadText">The text that will be inserted to the beginning of each line.</param>
        /// <param name="text">The text, which will be manipulated. The text can contain multiple lines.</param>
        /// <returns>The text with the leading text.</returns>
        public static string LineLead(string leadText, string text)
        {
            var buffer = new StringBuilder();
            var textReader = new StringReader(text);
            for (;;)
            {
                var line = textReader.ReadLine();
                if (line == null)
                    break;

                if (buffer.Length > 0)
                    buffer.Append(Environment.NewLine);

                buffer.Append(leadText).Append(line);
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Converts a text into the given type. If the type is a boolean, then the conversions in
        /// <see cref="BooleanUtilities"/> will be used.
        /// </summary>
        /// <typeparam name="T">The type to which the string is converted.</typeparam>
        /// <param name="s">The string, which is converted into the specified type.</param>
        /// <returns>A values representing the interpretation of the provided string.</returns>
        public static T ConvertString<T>(string s)
        {
            var tType = typeof(T);
            if (tType == typeof(bool))
            {
                object b = BooleanUtilities.ToBool(s);
                return (T)b;
            }
            return (T)Convert.ChangeType(s, tType);
        }

        /// <summary>
        /// Adds a block of text to a text buffer. Adding a block of text ensures that at least one blank line is added before the text. 
        /// However, if the text buffer is empty, then no empty lines will be added.
        /// </summary>
        /// <param name="textBlock">The block of text to be added.</param>
        /// <param name="buffer">The text buffer to which the text block will be added.</param>
        /// <returns>The text buffer appended with the text block.</returns>
        public static string AddTextBlock(string textBlock, string buffer)
        {
            if (buffer.Length == 0)
                return textBlock;

            if (string.IsNullOrWhiteSpace(textBlock))
                return buffer;

            if (!buffer.EndsWith(Environment.NewLine))
                buffer += Environment.NewLine;

            return buffer + Environment.NewLine + textBlock;
        }
    }
}
