using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.App.Common.Exceptions
{
    public sealed class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException () : base ()
        {
        }

        public EmailAlreadyExistsException( string message ) : base(message)
        {
        }

        public EmailAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public EmailAlreadyExistsException(Exception innerException) : base(innerException.Message, innerException)
        {
        }

    }
}
