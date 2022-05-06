using Corzbank.Data.Entities.Models;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Corzbank.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var result = _userService.GetUsers();

            return Ok(result.Result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var result = _userService.GetUserById(id);

            return Ok(result.Result);
        }

        [HttpPost]
        public IActionResult RegsiterUser([FromBody] UserModel user)
        {
            var result = _userService.RegisterUser(user);

            return Ok(result.Result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdsteUser(Guid id, [FromBody] UserModel user)
        {
            var result = _userService.UpdateUser(id, user);

            return Ok(result.Result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var result = _userService.DeleteUser(id);

            return Ok(result.Result);
        }
    }
}
