using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RecordCv.Helpers
{
    /// <summary>
    /// Provides string sanitisation utilities for preparing text values for SQL INSERT statements.
    /// </summary>
    public static class StringSanitiser
    {
        /// <summary>
        /// Normalises various Unicode quote and apostrophe characters to a standard single quote,
        /// then escapes single quotes for SQL by doubling them.
        /// </summary>
        /// <param name="input">The raw string to sanitise.</param>
        /// <returns>A SQL-safe string with normalised and escaped quotes.</returns>
        public static string ReplaceChar(string input)
        {
            string output = input
                .Replace("'", "\'")       // Replace curly/smart apostrophe placeholder
                .Replace('\u2018', '\'')  // ' left single quotation mark
                .Replace('\u2019', '\'')  // ' right single quotation mark
                .Replace('\u201A', '\'')  // ‚ single low-9 quotation mark
                .Replace('\u201B', '\'')  // ‛ single high-reversed-9 quotation mark
                .Replace('\u2032', '\'')  // ′ prime
                .Replace('\u00B4', '\'')  // ´ acute accent
                .Replace('`', '\'');      // ` grave accent

            return output.Replace("'", "''");
        }

        /// <summary>
        /// Strips all newline characters from a string and then applies <see cref="ReplaceChar"/>.
        /// </summary>
        /// <param name="input">The raw string to sanitise.</param>
        /// <returns>A single-line, SQL-safe string.</returns>
        public static string SanitiseMultiLine(string input)
        {
            var stripped = Regex.Replace(input, @"\r\n|\r|\n", string.Empty);
            return ReplaceChar(stripped);
        }
    }
}
