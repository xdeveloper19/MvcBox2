using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class OrderHasBox
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BoxId { get; set; }
        public Guid OrderId { get; set; }
        public bool IsBusy { get; set; }
    }
}
