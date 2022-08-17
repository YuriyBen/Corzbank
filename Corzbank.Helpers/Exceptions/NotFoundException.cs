using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Helpers.Exceptions
{
    public class NotFoundException : RequestException
    {
        public override int StatusCode => 404;
    }
}
