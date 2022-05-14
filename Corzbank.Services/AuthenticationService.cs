using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User user;

        public AuthenticationService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        public async Task<bool> ValidateUser(UserForLoginModel userForAuth)
        {
             user = await _userManager.FindByEmailAsync(userForAuth.Email);

            var result = await _userManager.CheckPasswordAsync(user, userForAuth.Password);

            return result;
        }


        public async Task<string> GenerateAccessToken(User user)
        {
            var signingCredentials = GetSigningCredentials("SECRET");
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, "Issuer", "Audience", "Expires", claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<string> GenerateRefreshToken()
        {
            var signingCredentials = GetSigningCredentials("SECRETKEYFORREFRESHTOKEN");
            var claims = await GetClaims(null);
            var tokenOptions = GenerateTokenOptions(signingCredentials, "IssuerRefresh", "AudienceRefresh", "ExpiresRefresh", claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials(string secretKey)
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(secretKey));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            if (user == null)
                return null;

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, string issuer, string audience, string expires, List<Claim> claims = null)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection(issuer).Value,
                audience: jwtSettings.GetSection(audience).Value,
                claims: claims,
                expires:
                DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection(expires).Value)),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}

