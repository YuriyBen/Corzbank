
using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Models.DTOs
{
    public class CardDetailsDTO : CardDTO
    {
        public Guid Id { get; set; }

        public string CardNumber { get; set; }

        public string ExpirationDate { get; set; }

        public ushort CvvCode { get; set; }

        public decimal Balance { get; set; }

        public bool IsActive { get; set; }
    }
}
