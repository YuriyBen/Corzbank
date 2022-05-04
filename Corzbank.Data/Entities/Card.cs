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
        [Required(ErrorMessage = "CardNumber is required")]
        [Range(16, 16, ErrorMessage = "Card length should be 16")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "ExpirationDate is required")]
        [Range(4, 4, ErrorMessage = "ExpirationDate length should be 4")]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "CvvCode is required")]
        [Range(3, 3, ErrorMessage = "CvvCode length should be 3")]
        public string CvvCode { get; set; }

        [Required(ErrorMessage = "CardType is required")]
        public CardType CardType { get; set; }

        [Required(ErrorMessage = "PaymnetSystem is required")]
        public PaymentSystem PaymentSystem { get; set; }

        public decimal Balance { get; set; }
        
        public bool IsActive { get; set; }
        
        [Required(ErrorMessage ="SecretWord is required")]
        public string SecretWord { get; set; }

        public User User { get; set; }
        
        public List<DepositCard> DepositCards { get; set; }

        [InverseProperty("SenderCard")]
        public List<TransferCard> SendTransfers { get; set; }

        [InverseProperty("ReceiverCard")]
        public List<TransferCard> ReceiveTransfers { get; set; }
        
    }
}
