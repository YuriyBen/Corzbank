using Corzbank.Data.Entities.Models;
using Corzbank.Services.Interfaces;
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

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public IActionResult GetCards()
        {
            var result = _cardService.GetCards();

            return Ok(result.Result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCardById(Guid id)
        {
            var result = _cardService.GetCardById(id);

            return Ok(result.Result);
        }

        [HttpPost]
        public IActionResult CreateCard([FromBody] CardModel card)
        {
            var result = _cardService.CreateCard(card);

            return Ok(result.Result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCard(Guid id)
        {
            var result = _cardService.DeleteCard(id);

            return Ok(result.Result);
        }
    }
}
