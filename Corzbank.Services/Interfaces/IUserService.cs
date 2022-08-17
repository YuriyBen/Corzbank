using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
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

        Task<Token> Login(UserForLoginDTO user);

        Task<IEnumerable<IdentityResult>> UpdateUser(Guid id, UserDTO userForUpdate);

        Task<IEnumerable<IdentityResult>> RegisterUser(UserDTO user);

        Task<bool> DeleteUser(Guid id);

        Task<Token> RefreshTokens(string refreshToken);
    }
}
