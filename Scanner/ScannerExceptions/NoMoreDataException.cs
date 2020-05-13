using System;

namespace Scanners.Exceptions
{

    public class NoMoreDataException : Exception
    {
        public NoMoreDataException(string message)
            :base(message)
        {
        }
    }
}