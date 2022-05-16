using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Services.Interfaces
{
    public interface IEmailRegistrationService
    {
        void SendEmail(string email, string subject, string message);
    }
}
