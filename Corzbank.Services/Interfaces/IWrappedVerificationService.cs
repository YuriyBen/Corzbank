using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IWrappedVerificationService
    {
        Task<bool> Verify(VerificationModel verificationModel);

        Task<bool> ConfirmVerification(ConfirmationModel confirmationModel);
    }
}
