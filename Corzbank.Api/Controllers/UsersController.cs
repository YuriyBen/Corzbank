using Corzbank.Data.Models.DTOs;
using Corzbank.Helpers;
using Corzbank.Services;
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
        private readonly IWrappedVerificationService _verificationService;
        public UsersController(IUserService userService, IForgotPasswordService forgotPasswordService, IWrappedVerificationService verificationService)
        {
            _userService = userService;
            _forgotPasswordService = forgotPasswordService;
            _verificationService = verificationService;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDTO user)
        {
            var result = await _userService.Login(user);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegsiterUser([FromBody] UserDTO user)
        {
            var result = await _userService.RegisterUser(user);

            if (result.ModelStateErrors(ModelState) != null)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdsteUser(Guid id, [FromBody] UserDTO user)
        {
            var result = await _userService.UpdateUser(id, user);

            if (result.ModelStateErrors(ModelState) != null)
            {
                return BadRequest(result.ModelStateErrors(ModelState));
            }

            return NoContent();
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
        public async Task<IActionResult> ForgotPassword(VerificationDTO verificationModel)
        {
            var result = await _verificationService.Verify(verificationModel);

            return Ok(result);
        }

        [HttpPost("confirm-verification")]
        public async Task<IActionResult> ConfirmVerification(ConfirmationDTO confirmationModel)
        {
            var result = await _verificationService.ConfirmVerification(confirmationModel);

            return Ok(result);
        }

        [HttpPost("set-new-password")]
        public async Task<IActionResult> SetNewPassword(SetNewPasswordDTO setNewPassword)
        {
            var result = await _forgotPasswordService.SetNewPassword(setNewPassword);

            return Ok(result);
        }

        [HttpPost("resend-verification")]
        public async Task<IActionResult> ResendVerification(VerificationDTO verificationModel)
        {
            var result = await _verificationService.Verify(verificationModel);

            return Ok(result);
        }
    }
}
