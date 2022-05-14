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
        private readonly GenericService<ForgotPasswordToken> _genericService;
        private readonly IEmailRegistrationService _emailService;

        public ForgotPasswordService(UserManager<User> userManager, GenericService<ForgotPasswordToken> genericService, IEmailRegistrationService emailService)
        {
            _emailService = emailService;
            _userManager = userManager;
            _genericService = genericService;
        }

        public async Task<ForgotPasswordToken> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var generatedCode = GenerateVerificationCode.GenerateCode();

                var forgotPasswordToken = new ForgotPasswordToken
                {
                    VerificationCode = generatedCode,
                    ValidTo = DateTime.Now.AddMinutes(10),
                    UserId = Guid.Parse(user.Id)
                };

                await _genericService.Insert(forgotPasswordToken);

                _emailService.SendEmail($"{user.Email}", "Reset Password",
                    @"
                    <div style='background: #f5ecec; padding: 5px; text-align: center;'>
                    Confirm Resetting Your Password
                    <p> Before you confirm the resetting, please verify the target address carefully
                    .If you confirm a reseting to an erroneous address, Corzbank will be unable to
                    assist in recovering the assets.If you understand the risks and can confirm
                    that this was your own action, use this code below:</p>
                    <p style = 'text-align: center; letter-spacing: 5px; font-size: xx-large;
                    font-weight: bold; background: radial-gradient(#515462, transparent) ;
                    color: #000b58' >" + $"{generatedCode}" + @" </p><p style = 'text-align: center;' >
                    If it wasn't you, dont pay attention for this email:)</p>
                    <span style = 'color: #808000; background-color: rgba(85, 78, 43, 0.1);'>Corzbank</span>
                "
                );

                return forgotPasswordToken;
            }

            return null;
        }

        public async Task<bool> ConfirmResettingPassword(string email, string verificationCode)
        {
            var user = await _userManager.FindByEmailAsync(email);

            ForgotPasswordToken verificationToken = await _genericService.FindByCondition(u => u.UserId == Guid.Parse(user.Id));

            if (verificationToken != null)
            {
                if (verificationToken.ValidTo > DateTime.Now && verificationToken.VerificationCode == verificationCode)
                {
                    verificationToken.IsVerified = true;
                    return true;
                }
            }

            return false;
        }

        public async Task SetNewPassword(string email, SetNewPasswordModel newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            ForgotPasswordToken verificationToken = await _genericService.FindByCondition(u => u.UserId == Guid.Parse(user.Id));

            if (verificationToken.IsVerified && newPassword != null)
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, newPassword.Password);
            }
        }
    }
}
