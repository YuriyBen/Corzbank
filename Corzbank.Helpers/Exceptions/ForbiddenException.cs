using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Helpers.Exceptions
{
    public class ForbiddenException: RequestException
    {
        public override int StatusCode => 403;
    }
}
