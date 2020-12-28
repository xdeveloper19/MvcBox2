using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class DriverHasBox
    {
        public Guid DriverId { get; set; }
        public Guid BoxId { get; set; }
    }
}
