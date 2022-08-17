using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Models
{
    public abstract class KeyEntity
    {
        [Key]
        public virtual Guid Id { get; set; }
    }
}
