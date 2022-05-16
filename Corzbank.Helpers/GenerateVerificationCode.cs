using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Helpers
{
    public static class GenerateVerificationCode
    {
        public static string GenerateCode()
        {
            Random rnd = new Random();

            var result = "";

            for(int i = 0; i < 6; i++)
            {
                result += rnd.Next(0, 9);
            }

            return result;
        }
    }
}
