using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Corzbank.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExchangesController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;

        public ExchangesController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
           var result = await _exchangeService.GetValues();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValueById(Guid id)
        {
            var result = await _exchangeService.GetValueById(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddValues() //TODO background job
        {
            var result = await _exchangeService.CreateExchage(); 

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateValues()
        {
            var result = await _exchangeService.UpdateExchage();

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteValues()
        {
           var result = await _exchangeService.DeleteExchange();

            return Ok(result);
        }
    }
}
