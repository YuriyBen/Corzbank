using Corzbank.Data.Models.DTOs;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corzbank.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepositsController : ControllerBase
    {
        private readonly IDepositService _depositService;
        private readonly IWrappedVerificationService _verificationService;

        public DepositsController(IDepositService depositService, IWrappedVerificationService verificationService)
        {
            _depositService = depositService;
            _verificationService = verificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeposits()
        {
            var result = await _depositService.GetDeposits();

            return Ok(result);
        }

        [Route("users/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetDepositsForUser(Guid id)
        {
            var result = await _depositService.GetDepositsForUser(id);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepositById(Guid id)
        {
            var result = await _depositService.GetDepositById(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> OpenDeposit([FromBody] DepositDTO card)
        {
            var result = await _depositService.OpenDeposit(card);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CloseDeposit(Guid id)
        {
            var result = await _depositService.CloseDeposit(id);

            return Ok(result);
        }

        [HttpPost("confirm-closing")]
        public async Task<IActionResult> ConfirmVerification(ConfirmationDTO confirmationModel)
        {
            var result = await _verificationService.ConfirmVerification(confirmationModel);

            return Ok(result);
        }
    }
}
