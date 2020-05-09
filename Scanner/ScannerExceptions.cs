using System;

namespace ScannerUtil.Exceptions
{
    public class NoMatchFoundException : Exception
    {
        public NoMatchFoundException()
        {
        }

        public NoMatchFoundException(string message)
            : base(message)
        {
        }
    }

    public class PassedEmptyStringException : Exception
    {
        public PassedEmptyStringException(string message)
            :base(message)
        {
        }
    }

}