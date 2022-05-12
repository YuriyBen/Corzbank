using Corzbank.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Corzbank.Helpers.Validations
{
    public class ValidateUser
    {
        private readonly ILogger<ValidateUser> _logger;
        private readonly UserManager<User> _userManager;
        public ValidateUser(ILogger<ValidateUser> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public bool FirstNameValid(User user)
        {
            if (user.Firstname != null)
            {
                const string expression = "[a-zA-Z]{0,20}$";

                if (StringToCheck(user.Firstname, expression))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool LastnameValid(User user)
        {
            if (user.Lastname != null)
            {
                const string expression = "[a-zA-Z]{0,20}$";

                if (StringToCheck(user.Lastname, expression))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool PhoneNumberValid(User user)
        {
            const string expression = "[+]?[0-9]{10,12}";

            if (user.PhoneNumber == null || user.PhoneNumber.Length < 9 || user.PhoneNumber.Length > 13 )
            {
                return false;
            }
            if (StringToCheck(user.PhoneNumber, expression))
            {
                if (_userManager.Users.Any(x => x.PhoneNumber == user.PhoneNumber && x.Id != user.Id))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool EmailValid(User user)
        {
            if (user.Email != null)
            {
                string email = user.Email.ToLower();
                const string expression = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

                if (StringToCheck(email, expression))
                {
                    if (_userManager.Users.Any(x => x.Email == user.Email && x.Id != user.Id) || user.Email == null)
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool StringToCheck(string toCheck, string expression)
        {
            if (Regex.IsMatch(toCheck, expression))
            {
                if (Regex.Replace(toCheck, expression, string.Empty).Length == 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public List<IdentityResult> UserIsValid(User user)
        {
            List<IdentityResult> validationErrors = new List<IdentityResult>();

            if (!FirstNameValid(user))
            {
                _logger.LogError($"FirstName: {user.Firstname} was failed");
                var errorMessage = new IdentityError
                {
                    Code = "firstName failed",
                    Description = $"FirstName: {user.Firstname} was failed"
                };

                var result = IdentityResult.Failed(errorMessage);
                validationErrors.Add(result);
            }

            if (!LastnameValid(user))
            {
                _logger.LogError($"LastName:{user.Lastname} was failed");
                var errorMessage = new IdentityError
                {
                    Code = "lastName failed",
                    Description = $"LastName:{user.Lastname} was failed"
                };

                var result = IdentityResult.Failed(errorMessage);
                validationErrors.Add(result);
            }

            if (!PhoneNumberValid(user))
            {
                _logger.LogError($"PhoneNumber: {user.PhoneNumber} was failed or it is already in use");
                var errorMessage = new IdentityError
                {
                    Code = "phoneNumber failed",
                    Description = $"PhoneNumber: {user.PhoneNumber} was failed or it is already in use"
                };

                var result = IdentityResult.Failed(errorMessage);
                validationErrors.Add(result);
            }

            if (!EmailValid(user))
            {
                _logger.LogError($"Email: {user.Email} was failed or it is already in use");
                var errorMessage = new IdentityError
                {
                    Code = "email failed",
                    Description = $"Email: {user.Email} was failed or it is already in use"
                };

                var result = IdentityResult.Failed(errorMessage);
                validationErrors.Add(result);
            }

            return validationErrors;
        }
    }
}
