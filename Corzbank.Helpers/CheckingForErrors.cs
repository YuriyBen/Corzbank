using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Helpers
{
    public static class CheckingForErrors
    {
        public static ModelStateDictionary ModelStateErrors(this IEnumerable<IdentityResult> result, ModelStateDictionary modelSate)
        {
            if (result != null)
            {
                foreach (var errorList in result)
                {
                    foreach (var error in errorList.Errors)
                    {
                        modelSate.AddModelError(error.Code, error.Description);
                    }
                }

                return modelSate;
            }
            return null;
        }
    }
}
