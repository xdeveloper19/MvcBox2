using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Alarm
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Acknowledge { get; set; }
        public string Message { get; set; }
        public DateTime AcknowledgedAt { get; set; }
        public bool Active { get; set; }
        public DateTime ReleasedAt { get; set; }
        public Guid AlarmTypeId {get;set;}
        public Guid BoxId { get; set; }
    }
}
