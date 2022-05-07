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
            const string expression = "[a-zA-Z]{0,20}$";

            if (StringToCheck(user.Firstname, expression))
            {
                return true;
            }
            return false;
        }

        public bool LastnameValid(User user)
        {
            const string expression = "[a-zA-Z]{0,20}$";

            if (StringToCheck(user.Lastname, expression))
            {
                return true;
            }
            return false;
        }

        public bool PhoneNumberValid(User user)
        {
            const string expression = "[+]?[0-9]{10,12}";

            if (user.PhoneNumber.Length < 9 || user.PhoneNumber.Length > 13)
            {
                return false;
            }
            if (StringToCheck(user.PhoneNumber, expression))
            {
                if(_userManager.Users.Any(x => x.PhoneNumber == user.PhoneNumber))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool EmailValid(User user)
        {
            string email = user.Email.ToLower();
            const string expression = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            if (StringToCheck(email, expression))
            {
                if (_userManager.Users.Any(x => x.Email == user.Email))
                {
                    return false;
                }
                return true;
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

        public bool UserIsValid(User user)
        {
            if (!FirstNameValid(user))
            {
                _logger.LogError($"FirstName: {user.Firstname} was failed");
                return false;
            }

            if (!LastnameValid(user))
            {
                _logger.LogError($"LastName:{user.Lastname} was failed");
                return false;
            }

            if (!PhoneNumberValid(user))
            {
                _logger.LogError($"PhoneNumber: {user.PhoneNumber} was failed or it is already in use");
                return false;
            }

            if (!EmailValid(user))
            {
                _logger.LogError($"Email: {user.Email} was failed or it is already in use");
                return false;
            }

            return true;
        }
    }
}
