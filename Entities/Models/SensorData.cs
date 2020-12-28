using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class SensorData
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SensorName { get; set; }
        public Guid SensorId { get; set; }
    }
}
