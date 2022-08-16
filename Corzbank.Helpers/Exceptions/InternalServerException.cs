using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Helpers.Exceptions
{
    public class InternalServerException : RequestException
    {
        public override int StatusCode => 500;
    }
}