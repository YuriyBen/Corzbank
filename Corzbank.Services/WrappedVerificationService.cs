using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
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
    public class WrappedVerificationService : IWrappedVerificationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IGenericRepository<Verification> _verificationRepo;
        private readonly IGenericRepository<Card> _cardRepo;
        private readonly IGenericRepository<Deposit> _depositRepo;
        private readonly IEmailRegistrationService _emailService;

        public WrappedVerificationService(UserManager<User> userManager, IGenericRepository<Verification> verificationRepo,
            IGenericRepository<Card> cardRepo, IGenericRepository<Deposit> depositRepo, IEmailRegistrationService emailService)
        {
            _emailService = emailService;
            _userManager = userManager;
            _verificationRepo = verificationRepo;
            _cardRepo = cardRepo;
            _depositRepo = depositRepo;
        }

        public async Task<bool> Verify(VerificationModel verificationModel)
        {
            var user = await _userManager.FindByEmailAsync(verificationModel.Email);

            if (user != null)
            {
                var generatedCode = GenerateVerificationCode.GenerateCode();

                var existingEmail = await _verificationRepo.GetQueryable().FirstOrDefaultAsync(fp => fp.User == user);

                if (existingEmail != null)
                {
                    await _verificationRepo.Remove(existingEmail);
                }

                var verification = new Verification
                {
                    VerificationCode = generatedCode,
                    ValidTo = DateTime.Now.AddMinutes(10),
                    User = user,
                    VerificationType = verificationModel.VerificationType,
                    CardId = verificationModel.CardId,
                    DepositId = verificationModel.DepositId
                };

                await _verificationRepo.Insert(verification);

                _emailService.SendEmail($"{user.Email}", $"{verificationModel.VerificationType} Verification",
                    @"
                    <div style='background: #f5ecec; padding: 5px; text-align: center;'>" +
                    $"{ verificationModel.VerificationType} Verification" +
                    @"<p> Before you confirm the operation, please verify the target address carefully
                    .If you confirm operation to an erroneous address, Corzbank will be unable to
                    assist in recovering the assets.If you understand the risks and can confirm
                    that this was your own action, use this code below:</p>
                    <p style = 'text-align: center; letter-spacing: 5px; font-size: xx-large;
                    font-weight: bold; background: radial-gradient(#515462, transparent) ;
                    color: #000b58' >" + $"{generatedCode}" + @" </p><p style = 'text-align: center;' >
                    If it wasn't you, dont pay attention for this email:)</p>
                    <span style = 'color: #808000; background-color: rgba(85, 78, 43, 0.1);'>Corzbank</span>
                "
                );

                return true;
            }

            return false;
        }

        public async Task<bool> ConfirmVerification(ConfirmationModel confirmationModel)
        {
            Verification verification = null;

            if (confirmationModel.VerificationType == VerificationType.Email || confirmationModel.VerificationType == VerificationType.ResetPassword)
                verification = await _verificationRepo.GetQueryable().FirstOrDefaultAsync(v => v.User.Email == confirmationModel.Email);
            else if (confirmationModel.VerificationType == VerificationType.CloseCard)
                verification = await _verificationRepo.GetQueryable().FirstOrDefaultAsync(v => v.CardId == confirmationModel.CardId);
            else if (confirmationModel.VerificationType == VerificationType.CloseDeposit)
                verification = await _verificationRepo.GetQueryable().FirstOrDefaultAsync(v => v.DepositId == confirmationModel.DepositId);

            if (verification == null || verification.ValidTo < DateTime.Now || verification.VerificationCode != confirmationModel.VerificationCode)
                return false;

            verification.IsVerified = true;

            await _verificationRepo.Update(verification);

            if (verification.VerificationType == VerificationType.Email || verification.VerificationType == VerificationType.ResetPassword)
            {
                var user = await _userManager.FindByEmailAsync(confirmationModel.Email);

                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }
            else if (verification.VerificationType == VerificationType.CloseCard)
            {
                var card = await _cardRepo.GetQueryable().FirstOrDefaultAsync(v => v.Id == verification.CardId);
                card.IsActive = false;

                await _cardRepo.Update(card);
            }
            else if (verification.VerificationType == VerificationType.CloseDeposit)
            {
                var deposit = await _depositRepo.GetQueryable().Include(c => c.Card).FirstOrDefaultAsync(v => v.Id == verification.DepositId);
                deposit.Status = DepositStatus.Closed;

                await _depositRepo.Update(deposit);

                var card = await _cardRepo.GetQueryable().FirstOrDefaultAsync(c => c.Id == deposit.Card.Id);
                card.Balance += deposit.Amount;

                await _cardRepo.Update(card);
            }

            if (verification.VerificationType != VerificationType.ResetPassword)
                await _verificationRepo.Remove(verification);

            return true;
        }
    }
}
