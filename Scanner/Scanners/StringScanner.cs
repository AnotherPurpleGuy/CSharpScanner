using Scanners.Exceptions;

namespace Scanners
{
    public sealed class StringScanner : Scanner
    {
        /// <summary>
        /// The StringScanner class takes a single string as it's input and then
        /// allows you to return parts of that string by using the next
        /// calls. This can be very useful when processing user inputs and you
        /// want to ignore any extra details they might have added e.g. unwanted
        /// text.
        /// </summary>
        /// <param name="inputString"></param>
        public StringScanner(string inputString)
        {
            if (inputString.Equals("")) throw new InvalidArgumentException("Empty string was handed to constructor");
            _working_string = inputString;
            SetMatchs(inputString,Patten.NEW_LINE_PATTEN);
        }
    }
}