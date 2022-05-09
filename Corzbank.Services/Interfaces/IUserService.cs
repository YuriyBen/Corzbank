using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUserById(Guid id);

        Task<User> UpdateUser(Guid id, UserModel userForUpdate);

        Task<User> RegisterUser(UserModel user);

        Task<bool> DeleteUser(Guid id);
    }
}
