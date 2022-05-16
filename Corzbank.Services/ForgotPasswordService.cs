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

        public ForgotPasswordService(UserManager<User> userManager, GenericService<Verification> genericService)
        {
            _userManager = userManager;
            _genericService = genericService;
        }

        public async Task<bool> SetNewPassword(SetNewPasswordModel newPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(newPasswordModel.Email);

            Verification verificationToken = await _genericService.FindByCondition(u => u.User.Id == user.Id);

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
