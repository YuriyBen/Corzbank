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

        public UserService(IMapper mapper, UserManager<User> userManager, ValidateUser validateUser)
        {
            _mapper = mapper;
            _userManager = userManager;
            _validateUser = validateUser;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> RegisterUser(UserModel userForRegistration)
        {
            if (userForRegistration != null)
            {
                var user = _mapper.Map<User>(userForRegistration);

                if (_validateUser.UserIsValid(user))
                {
                    await _userManager.CreateAsync(user, userForRegistration.Password);

                    return user;
                }
            }
            return null;
        }

        public async Task<User> UpdateUser(Guid id, UserModel userForUpdate)
        {
            if (userForUpdate != null)
            {
                var user = await GetUserById(id);
                var mappedUser = _mapper.Map(userForUpdate, user);

                await _userManager.UpdateAsync(mappedUser);

                return mappedUser;
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
