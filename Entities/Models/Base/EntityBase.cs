using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models.Base
{
    public class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
