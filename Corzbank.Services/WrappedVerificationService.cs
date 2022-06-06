using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
using Corzbank.Helpers;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class WrappedVerificationService: IWrappedVerificationService
    {
        private readonly UserManager<User> _userManager;
        private readonly GenericService<Verification> _genericService;
        private readonly GenericService<Card> _genericCardService;
        private readonly GenericService<Deposit> _genericDepositService;
        private readonly IEmailRegistrationService _emailService;

        public WrappedVerificationService(UserManager<User> userManager, GenericService<Verification> genericService,
            IEmailRegistrationService emailService, GenericService<Card> genericCardService, GenericService<Deposit> genericDepositService)
        {
            _emailService = emailService;
            _userManager = userManager;
            _genericService = genericService;
            _genericCardService = genericCardService;
            _genericDepositService = genericDepositService;
        }

        public async Task<bool> Verify(VerificationModel verificationModel)
        {
            var user = await _userManager.FindByEmailAsync(verificationModel.Email);

            if (user != null)
            {
                var generatedCode = GenerateVerificationCode.GenerateCode();

                var existingEmail = await _genericService.FindByCondition(fp => fp.User == user);

                if (existingEmail != null)
                {
                    await _genericService.Remove(existingEmail);
                }

                var verification = new Verification
                {
                    VerificationCode = generatedCode,
                    ValidTo = DateTime.Now.AddMinutes(10),
                    User = user,
                    VerificationType = verificationModel.VerificationType,
                    CardId = verificationModel.CardId
                };

                await _genericService.Insert(verification);

                _emailService.SendEmail($"{user.Email}", $"{verificationModel.VerificationType} Verification",
                    @"
                    <div style='background: #f5ecec; padding: 5px; text-align: center;'>"+
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
            var user = await _userManager.FindByEmailAsync(confirmationModel.Email);

            Verification verification = await _genericService.FindByCondition(u => u.User == user);

            if (verification != null)
            {
                if (verification.ValidTo > DateTime.Now && verification.VerificationCode == confirmationModel.VerificationCode)
                {
                    verification.IsVerified = true;

                    await _genericService.Update(verification);

                    if (verification.VerificationType == VerificationType.Email)
                    {
                        user.EmailConfirmed = true;
                        await _userManager.UpdateAsync(user);

                        await _genericService.Remove(verification);
                    }
                    else if (verification.VerificationType == VerificationType.CloseCard)
                    {
                        var card = await _genericCardService.Get(verification.CardId);
                        card.IsActive = false;

                        await _genericCardService.Update(card);

                        await _genericService.Remove(verification);
                    }
                    else if (verification.VerificationType == VerificationType.CloseDeposit)
                    {
                        var deposit = await _genericDepositService.Get(verification.DepositId);
                        deposit.IsActive = DepositStatus.Closed;

                        await _genericDepositService.Update(deposit);

                        await _genericService.Remove(verification);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
