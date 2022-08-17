using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IForgotPasswordService
    {
        Task<bool> SetNewPassword(SetNewPasswordDTO newPassword);
    }
}
