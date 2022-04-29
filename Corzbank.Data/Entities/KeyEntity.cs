using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Entities
{
    public abstract class KeyEntity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
