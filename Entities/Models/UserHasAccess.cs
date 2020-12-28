using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class UserHasAccess
    {
        public string UserId { get; set; }
        public Guid BoxId { get; set; }
    }
}
