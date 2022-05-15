using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Helpers;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class ForgotPasswordService: IForgotPasswordService
    {
        private readonly UserManager<User> _userManager;
        private readonly GenericService<Verification> _genericService;
        private readonly IEmailRegistrationService _emailService;

        public ForgotPasswordService(UserManager<User> userManager, GenericService<Verification> genericService, IEmailRegistrationService emailService)
        {
            _emailService = emailService;
            _userManager = userManager;
            _genericService = genericService;
        }

        public async Task<bool> ConfirmResettingPassword(ConfirmationModel confirmationModel)
        {
            var user = await _userManager.FindByEmailAsync(confirmationModel.Email);

            Verification verificationToken = await _genericService.FindByCondition(u => u.UserId == Guid.Parse(user.Id));

            if (verificationToken != null)
            {
                if (verificationToken.ValidTo > DateTime.Now && verificationToken.VerificationCode == confirmationModel.VerificationCode)
                {
                    verificationToken.IsVerified = true;

                    await _genericService.Update(verificationToken);

                    return true;
                }
            }

            return false;
        }

        public async Task<bool> SetNewPassword(SetNewPasswordModel newPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(newPasswordModel.Email);

            Verification verificationToken = await _genericService.FindByCondition(u => u.UserId == Guid.Parse(user.Id));

            if (verificationToken.IsVerified && newPasswordModel != null)
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, newPasswordModel.Password);

                await _genericService.Remove(verificationToken);
                return true;
            }

            return false;
        }
    }
}
