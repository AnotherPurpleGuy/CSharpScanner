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

    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException(string message)
            :base(message)
        {
        }
    }

    public class NoMoreDataException : Exception
    {
        public NoMoreDataException(string message)
            :base(message)
        {
        }
    }
}