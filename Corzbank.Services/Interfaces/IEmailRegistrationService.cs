using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IEmailRegistrationService
    {
        void SendEmail(string email, string subject, string message);
    }
}
