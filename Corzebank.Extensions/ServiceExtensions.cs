using Corzbank.Data;
using Corzbank.Data.Entities;
using Corzbank.Helpers.Validations;
using Corzbank.Services;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransferService, TransferService>();
            services.AddScoped<IDepositService, DepositService>();
            services.AddScoped<IExchangeService, ExchangeService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
            services.AddScoped<IEmailRegistrationService, EmailRegistrationService>();
            services.AddScoped<IWrappedVerificationService, WrappedVerificationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(GenericService<>));
            services.AddScoped<ILoggerManager, LoggerManager>();

            services.AddScoped<ValidateUser>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequiredLength = 6;
                o.User.RequireUniqueEmail = true;
            })
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<User, Role, CorzbankDbContext, Guid>>()
            .AddRoleStore<RoleStore<Role, CorzbankDbContext, Guid>>();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        ValidAudience = jwtSettings.GetSection("Audience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });
        }
    }
}
