using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class UserHasOrder
    {
        public string UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}
