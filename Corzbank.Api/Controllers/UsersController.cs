using Corzbank.Data.Entities.Models;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Corzbank.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _userService.GetUserById(id);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegsiterUser([FromBody] UserModel user)
        {
            var result = await _userService.RegisterUser(user);

            if (result != null)
            {
                foreach (var errorList in result)
                {
                    foreach (var error in errorList.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }

                return BadRequest(ModelState);
            }

            return Ok("User was successfully registered");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdsteUser(Guid id, [FromBody] UserModel user)
        {
            var result = await _userService.UpdateUser(id, user);

            if (result != null)
            {
                foreach (var errorList in result)
                {
                    foreach (var error in errorList.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }

                return BadRequest(ModelState);
            }

            return Ok("User was successfully updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUser(id);

            return Ok(result);
        }
    }
}
