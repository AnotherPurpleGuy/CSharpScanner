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

        // Contains the match for the next item to be returned
        private Match _next_match;
        
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

            updateLineData();
            return _current_match.Groups["line"].Value;
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

        private void updateLineData()
        {

            Regex rx = new Regex(NEWLINEPATTEN,
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if(_current_match == null)
            {
                _current_match = rx.Match(_working_string);
                _working_string = _working_string.Substring(_current_match.Groups[0].Length);
                _next_match = rx.Match(_working_string);
                _working_string = _working_string.Substring(_next_match.Groups[0].Length);
            } else 
            {
                _current_match = _next_match;
                _next_match = rx.Match(_working_string);
                _working_string = _working_string.Substring(_next_match.Groups[0].Length);
            }
        }
    }
}
