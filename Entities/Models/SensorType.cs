using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class SensorType
    {
        public SensorType()
        {
            this.Sensors = new List<Sensor>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public ICollection<Sensor> Sensors { get; set; }
    }
}
