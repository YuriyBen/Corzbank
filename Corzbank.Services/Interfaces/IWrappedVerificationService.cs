using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IWrappedVerificationService
    {
        Task<bool> Verify(VerificationDTO verificationModel);

        Task<bool> ConfirmVerification(ConfirmationDTO confirmationModel);
    }
}
