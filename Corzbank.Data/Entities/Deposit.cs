using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Deposit: KeyEntity
    {
        [Required]
        public decimal Amount { get; set; }
        
        public int Duration { get; set; }
        
        public double APY { get; set; }

        
        public List<DepositCard> DepositCards { get; set; }
    }
}
