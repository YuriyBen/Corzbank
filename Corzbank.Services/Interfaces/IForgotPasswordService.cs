using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IForgotPasswordService
    {
        Task<ForgotPasswordToken> ForgotPassword(string email);

        Task<bool> ConfirmResettingPassword(string email, string verificationCode);

        Task SetNewPassword(string email, SetNewPasswordModel newPassword);
    }
}
