using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class EventType
    {
        public EventType()
        {
            this.Events = new List<Event>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(45)]
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
