using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Card: KeyEntity
    {
        [Required]
        [StringLength(16, ErrorMessage = "Card length should be 16")]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "ExpirationDate length should be 4")]
        public string ExpirationDate { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "CvvCode length should be 3")]
        public string CvvCode { get; set; }

        [Required]
        public CardType CardType { get; set; }

        [Required]
        public PaymentSystem PaymentSystem { get; set; }

        public decimal Balance { get; set; } = 0;
        
        [Required]
        public bool IsActive { get; set; }
        
        [Required]
        public string SecretWord { get; set; }



        [Required]
        public User User { get; set; }
        
        public List<DepositCard> DepositCards { get; set; }

      
        [InverseProperty("SenderCard")]
        public List<TransferCard> SendTransfers { get; set; }

       
        [InverseProperty("ReceiverCard")]
        public List<TransferCard> ReceiveTransfers { get; set; }
        
    }
}
