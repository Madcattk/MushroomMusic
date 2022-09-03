using System;

namespace _6314110007
{
    // Exception class for duplicate item errors
    // in search tree insertions.
    public class DuplicateItemException : ApplicationException
    {
        // Construct this exception object.
        // message is the error message.
        public DuplicateItemException( string message ) : base( message )
        {
        }
    }
}
