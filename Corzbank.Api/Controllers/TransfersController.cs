using Corzbank.Data.Entities.Models;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    public class TransfersController : ControllerBase
    {
        private readonly ITransferService _transferService;

        public TransfersController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransfers()
        {
            var result = await _transferService.GetTransfers();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransfer(Guid id)
        {
            var result = await _transferService.GetTransferById(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransfer([FromBody] TransferModel transfer)
        {
            var result = await _transferService.CreateTransfer(transfer);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransfer(Guid id)
        {
            var result = await _transferService.DeleteTransfer(id);

            return Ok(result);
        }
    }
}
