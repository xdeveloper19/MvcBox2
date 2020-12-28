using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class VariableNotify
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid VariableId { get; set; }
    }
}
