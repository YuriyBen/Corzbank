using Corzbank.Data.Models.DTOs;
using Corzbank.Services;
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
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IWrappedVerificationService _verificationService;

        public CardsController(ICardService cardService, IWrappedVerificationService verificationService)
        {
            _cardService = cardService;
            _verificationService = verificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            var result = await _cardService.GetCards();

            return Ok(result);
        }

        [Route("users/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCardsForUser(Guid id)
        {
            var result = await _cardService.GetCardsForUser(id);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById(Guid id)
        {
            var result = await _cardService.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] CardDTO card)
        {
            var result = await _cardService.CreateCard(card);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CloseCard(Guid id)
        {
            var result = await _cardService.CloseCard(id);

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
