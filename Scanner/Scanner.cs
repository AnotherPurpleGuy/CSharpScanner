using System;
using System.Text.RegularExpressions;

namespace ScannerUtil
{
    public class Scanner
    {
        // Fields

        // Copy of the original string handed to the GivenString Constructor
        private string _working_string;
        
        // Regex for identifying a single line
        private string NEWLINEPATTEN = @"(?<line>[^\t\n\v\r]*)(\t|\n|\v|\r)*";

        // Continan the matches that will be returned by a next* method call
        private Match _current_match;
        
        // Constructors

        /// <summary>
        /// Constructs a new <code>Scanner</code> that takes a given string that
        /// can then have further operations performend on it.
        /// </summary>
        /// <param name="inputString">String given to the constructor</param>
        public Scanner(string inputString)
        {
            _working_string = inputString;
        }

        // Methods

        /// <summary>
        /// This method will return the next line from the input made in the
        /// constructor. Line are seporated the the \n char.
        /// </summary>
        /// <returns></returns>
        public string nextLine()
        {

            Regex rx = new Regex(NEWLINEPATTEN,
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            _current_match = rx.Match(_working_string);

            GroupCollection groups = _current_match.Groups;

            _working_string = _working_string.Substring(groups[0].Length);

            Console.WriteLine(groups["line"].Value);

            return groups["line"].Value;

        }

        /// <summary>
        /// Method checks to see if there is another line to be returned after
        /// the current one. Note: this method dose not shift the possition of
        /// the current line, only the <code>nextLine()</code> can do that.
        /// </summary>
        /// <returns></returns>
        public bool hasNextLine()
        {
            return true;
        }
    }
}
