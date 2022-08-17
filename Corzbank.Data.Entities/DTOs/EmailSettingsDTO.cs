using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Models.DTOs
{
    public class EmailSettingsDTO
    {
        public string Server { get; set; }

        public int Port { get; set; }

        public string Name { get; set; }

        public string FromAddress { get; set; }

        public string Password { get; set; }
    }
}
