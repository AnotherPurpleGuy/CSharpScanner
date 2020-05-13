using Scanners.Exceptions;

namespace Scanners
{
    public sealed class StringScanner : Scanner
    {
        public StringScanner(string inputString)
        {
            if (inputString.Equals("")) throw new InvalidArgumentException("Empty string was handed to constructor");
            _working_string = inputString;
            setMatchs(inputString,Patten.NEW_LINE_PATTEN);
        }
    }
}