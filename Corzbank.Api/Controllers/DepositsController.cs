using Corzbank.Data.Entities.Models;
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
    [Authorize]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepositById(Guid id)
        {
            var result = await _depositService.GetDepositById(id);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> OpenDeposit([FromBody] DepositModel card)
        {
            var result = await _depositService.OpenDeposit(card);

            return Ok(result);
        }

        [HttpPost("confirm-closing")]
        public async Task<IActionResult> ConfirmVerification(ConfirmationModel confirmationModel)
        {
            var result = await _verificationService.ConfirmVerification(confirmationModel);

            return Ok(result);
        }
    }
}
