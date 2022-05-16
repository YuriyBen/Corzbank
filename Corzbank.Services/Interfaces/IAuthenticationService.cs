using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> ValidateUser(UserForLoginModel userForAuth);

        Task<string> GenerateAccessToken(User user);
        Task<string> GenerateRefreshToken();
    }
}
