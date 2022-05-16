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

        public DepositsController(IDepositService depositService)
        {
            _depositService = depositService;
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

        [HttpPost]
        public async Task<IActionResult> OpenDeposit([FromBody] DepositModel card)
        {
            var result = await _depositService.OpenDeposit(card);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeposit(Guid id)
        {
            var result = await _depositService.DeleteDeposit(id);

            return Ok(result);
        }
    }
}
