﻿using System;

namespace Entities.Models
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid BoxId { get; set; }
        public Guid EventTypeId { get; set; }
    }
}
