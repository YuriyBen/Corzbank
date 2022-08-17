using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Models.DTOs.Details
{
    public class TransferDetails : TransferDTO
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public bool IsSuccessful { get; set; }

        public Guid? ReceiverCardId { get; set; }
    }
}
