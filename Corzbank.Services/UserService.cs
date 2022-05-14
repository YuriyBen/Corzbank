using AutoMapper;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Helpers.Validations;
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
        private readonly GenericService<Token> _genericService;

        public UserService(IMapper mapper, UserManager<User> userManager, ValidateUser validateUser, IAuthenticationService authenticationService, GenericService<Token> genericService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _userManager = userManager;
            _validateUser = validateUser;
            _genericService = genericService;
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

        public async Task<Token> Login(UserForLoginModel userForLogin)
        {

            var result = await _authenticationService.ValidateUser(userForLogin);

            if (result)
            {
                var user = await _userManager.FindByEmailAsync(userForLogin.Email);

                var tokens = new Token
                {
                    AccessToken = await _authenticationService.GenerateAccessToken(user),
                    RefreshToken = await _authenticationService.GenerateRefreshToken(),
                    User = user
                };

                await _genericService.Insert(tokens);

                return tokens;
            }
            return null;
        }

        public async Task<IEnumerable<IdentityResult>> RegisterUser(UserModel userForRegistration)
        {
            if (userForRegistration != null)
            {
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

                    if (!validUser.Succeeded)
                    {
                        validationErrors.Add(validUser);
                        return validationErrors;
                    }
                }

                if (validationErrors.Count > 0)
                    return validationErrors;
            }

            return null;
        }

        public async Task<IEnumerable<IdentityResult>> UpdateUser(Guid id, UserModel userForUpdate)
        {
            if (userForUpdate != null)
            {
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
            }
            return null;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await GetUserById(id);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);

                return true;
            }
            return false;
        }
    }
}
