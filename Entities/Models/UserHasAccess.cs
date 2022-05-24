using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class UserHasAccess
    {
        public Guid OwnerId { get; set; }
        public Guid BoxId { get; set; }
    }
}
