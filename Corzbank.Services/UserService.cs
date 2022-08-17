using AutoMapper;
using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
using Corzbank.Data.Enums;
using Corzbank.Helpers.Validations;
using Corzbank.Repository.Interfaces;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ValidateUser _validateUser;
        private readonly IAuthenticationService _authenticationService;
        private readonly IGenericRepository<Token> _tokenRepo;
        private readonly IWrappedVerificationService _verificationService;

        public UserService(IMapper mapper, UserManager<User> userManager, ValidateUser validateUser, IAuthenticationService authenticationService,
            IGenericRepository<Token> tokenRepo, IWrappedVerificationService verificationService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _userManager = userManager;
            _validateUser = validateUser;
            _tokenRepo = tokenRepo;
            _verificationService = verificationService;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var result = await _userManager.FindByIdAsync(id.ToString());

            return result;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var result = await _userManager.Users.ToListAsync();

            return result;
        }

        public async Task<Token> Login(UserForLoginDTO userForLogin)
        {
            var userIsValid = await _authenticationService.ValidateUser(userForLogin);

            if (!userIsValid)
                return null;

            var user = await _userManager.FindByEmailAsync(userForLogin.Email);

            if (!user.EmailConfirmed)
                return null;

            var tokenForDeleting = await _tokenRepo.GetQueryable().FirstOrDefaultAsync(x => x.User.Id == user.Id);
            if (tokenForDeleting != null)
                await _tokenRepo.Remove(tokenForDeleting);

            var tokens = new Token
            {
                AccessToken = await _authenticationService.GenerateAccessToken(user),
                RefreshToken = await _authenticationService.GenerateRefreshToken(),
                User = user
            };

            await _tokenRepo.Insert(tokens);

            return tokens;
        }

        public async Task<IEnumerable<IdentityResult>> RegisterUser(UserDTO userForRegistration)
        {
            if (userForRegistration == null)
                return null;

            var mappedUser = _mapper.Map<User>(userForRegistration);

            var validationErrors = _validateUser.UserIsValid(mappedUser);


            var validators = _userManager.PasswordValidators;

            foreach (var validator in validators)
            {
                var validPassword = await validator.ValidateAsync(_userManager, null, userForRegistration.Password);

                if (!validPassword.Succeeded)
                {
                    validationErrors.Add(validPassword);
                }
            }

            if (validationErrors.Count == 0)
            {
                var validUser = await _userManager.CreateAsync(mappedUser, userForRegistration.Password);

                var verificationModel = new VerificationDTO
                {
                    Email = mappedUser.Email,
                    VerificationType = VerificationType.Email
                };

                await _verificationService.Verify(verificationModel);

                if (!validUser.Succeeded)
                {
                    validationErrors.Add(validUser);
                    return validationErrors;
                }
            }

            if (validationErrors.Count > 0)
                return validationErrors;

            return null;
        }

        public async Task<IEnumerable<IdentityResult>> UpdateUser(Guid id, UserDTO userForUpdate)
        {
            if (userForUpdate == null)
                return null;

            var user = await GetUserById(id);
            var mappedUser = _mapper.Map(userForUpdate, user);

            var validationErrors = _validateUser.UserIsValid(mappedUser);

            var validators = _userManager.PasswordValidators;

            foreach (var validator in validators)
            {
                var validPassword = await validator.ValidateAsync(_userManager, null, userForUpdate.Password);

                if (!validPassword.Succeeded)
                {
                    validationErrors.Add(validPassword);
                }
            }

            var validUser = await _userManager.UpdateAsync(mappedUser);

            if (!validUser.Succeeded)
            {
                validationErrors.Add(validUser);
                return validationErrors;
            }

            if (validationErrors.Count > 0)
                return validationErrors;

            return null;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await GetUserById(id);

            if (user == null)
                return false;

            await _userManager.DeleteAsync(user);
            return true;
        }

        public async Task<Token> RefreshTokens(string refreshToken)
        {
            var token = await _tokenRepo.GetQueryable().FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);
            var user = await _userManager.FindByIdAsync(token.User.Id.ToString());

            if (token == null)
                return null;

            var generatedAccessToken = await _authenticationService.GenerateAccessToken(user);
            var generatedRefreshToken = await _authenticationService.GenerateRefreshToken();

            TokenDTO newlyToken = new TokenDTO
            {
                AccessToken = generatedAccessToken,
                RefreshToken = generatedRefreshToken,
                User = token.User
            };

            var mappedTokens = _mapper.Map(newlyToken, token);

            await _tokenRepo.Update(mappedTokens);

            return mappedTokens;
        }
    }
}
