using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Helpers.Exceptions
{
    public class RequestException : ApplicationException
    {
        public RequestException() { }

        public RequestException(string message) : base(message) { }

        public virtual int StatusCode => 500;
    }
}
