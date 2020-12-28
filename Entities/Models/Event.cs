using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(45)]
        public string Type { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid BoxId { get; set; }
    }
}
