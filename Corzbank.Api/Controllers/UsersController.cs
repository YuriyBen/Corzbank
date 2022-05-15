using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Helpers;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IForgotPasswordService _forgotPasswordService;
        public UsersController(IUserService userService, IForgotPasswordService forgotPasswordService)
        {
            _userService = userService;
            _forgotPasswordService = forgotPasswordService;
        }

        [HttpGet, Authorize]
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginModel user)
        {
            var result = await _userService.Login(user);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegsiterUser([FromBody] UserModel user)
        {
            var result = await _userService.RegisterUser(user);

            if (result.CheckForaErrors(ModelState) != null)
            {
                return BadRequest(result);
            }

            return Ok("User was successfully registered");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdsteUser(Guid id, [FromBody] UserModel user)
        {
            var result = await _userService.UpdateUser(id, user);

            if (result.CheckForaErrors(ModelState) != null)
            {
                return BadRequest(result.CheckForaErrors(ModelState));
            }

            return Ok("User was successfully updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUser(id);

            return Ok(result);
        }

        [HttpPost("refresh-tokens")]
        public async Task<IActionResult> RefreshTokens(string refreshToken)
        {
            var result = await _userService.RefreshTokens(refreshToken);

            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _forgotPasswordService.ForgotPassword(email);

            return Ok(result);
        }

        [HttpPost("confirm-resetting-password")]
        public async Task<IActionResult> ConfirmResettingPassword(ConfirmationModel confirmationModel)
        {
            var result = await _forgotPasswordService.ConfirmResettingPassword(confirmationModel);

            return Ok(result);
        }

        [HttpPost("set-new-password")]
        public async Task<IActionResult> SetNewPassword(SetNewPasswordModel setNewPassword)
        {
            var result = await _forgotPasswordService.SetNewPassword(setNewPassword);

            return Ok(result);
        }
    }
}
