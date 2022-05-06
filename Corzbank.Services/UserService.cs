using AutoMapper;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly GenericService<User> _genericService;
        public UserService(IMapper mapper, GenericService<User> genericService)
        {
            _mapper = mapper;
            _genericService = genericService;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _genericService.GetByGuid(id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _genericService.GetRange();
        }

        public async Task<User> RegisterUser(UserModel userForRegistration)
        {

            if (userForRegistration != null)
            {
                var user = _mapper.Map<User>(userForRegistration);

                await _genericService.Insert(user);

                return user;
            }
            return null;
        }

        public async Task<User> UpdateUser(Guid id, UserModel userForUpdate)
        {
            if (userForUpdate != null)
            {
                var user = GetUserById(id).Result;
                user = _mapper.Map(userForUpdate, user);

                await _genericService.Update(user);

                return user;
            }
            return null;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await GetUserById(id);

            if (user != null)
            {
                await _genericService.Remove(user);

                return true;
            }
            return false;
        }
    }
}
