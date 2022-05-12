using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Microsoft.AspNetCore.Identity;
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

        Task<IEnumerable<IdentityResult>> UpdateUser(Guid id, UserModel userForUpdate);

        Task<IEnumerable<IdentityResult>> RegisterUser(UserModel user);

        Task<bool> DeleteUser(Guid id);
    }
}
