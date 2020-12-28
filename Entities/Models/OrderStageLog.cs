using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class OrderStageLog
    {
        public Guid OrderId { get; set; }
        public Guid OrderStageId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
