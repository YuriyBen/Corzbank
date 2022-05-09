using Corzbank.Data.Entities.Models;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corzbank.Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private readonly ITransferService _transferService;

        public TransfersController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpGet]
        public IActionResult GetTransfers()
        {
            var result = _transferService.GetTransfers();

            return Ok(result.Result);
        }

        [HttpGet("{id}")]
        public IActionResult GetTransfer(Guid id)
        {
            var result = _transferService.GetTransferById(id);

            return Ok(result.Result);
        }

        [HttpPost]
        public IActionResult CreateTransfer([FromBody] TransferModel transfer)
        {
            var result = _transferService.CreateTransfer(transfer);

            return Ok(result.Result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransfer(Guid id)
        {
            var result = _transferService.DeleteTransfer(id);

            return Ok(result.Result);
        }
    }
}
