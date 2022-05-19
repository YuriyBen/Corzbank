using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities.Models
{
    public class BackgroundJobModel
    {
        public bool IsActive { get; set; }

        public int IntervalMinutes { get; set; }
    }
}
