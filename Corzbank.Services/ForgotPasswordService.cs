using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Helpers;
using Corzbank.Repository.Interfaces;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly UserManager<User> _userManager;
        private readonly IGenericRepository<Verification> _verificationRepo;

        public ForgotPasswordService(UserManager<User> userManager, IGenericRepository<Verification> verificationRepo)
        {
            _userManager = userManager;
            _verificationRepo = verificationRepo;
        }

        public async Task<bool> SetNewPassword(SetNewPasswordModel newPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(newPasswordModel.Email);

            Verification verificationToken = await _verificationRepo.GetQueryable().FirstOrDefaultAsync(u => u.User.Id == user.Id);

            if (verificationToken.IsVerified && newPasswordModel != null)
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, newPasswordModel.Password);

                await _verificationRepo.Remove(verificationToken);
                return true;
            }

            return false;
        }
    }
}
