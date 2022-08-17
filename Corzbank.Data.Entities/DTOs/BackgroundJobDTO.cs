using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Models.DTOs
{
    public class BackgroundJobDTO
    {
        public bool IsActive { get; set; }

        public int IntervalMinutes { get; set; }
    }
}
