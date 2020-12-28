using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class Sensor
    {
        public Sensor()
        {
            this.SensorDatas = new List<SensorData>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(45)]
        public string Name { get; set; }
        [MaxLength(64)]
        public string Address { get; set; }
        [MaxLength(64)]
        public string Bus { get; set; }
        public Guid SensorTypeId { get; set; }
        public Guid BoxId { get; set; }
        public ICollection<SensorData> SensorDatas { get; set; }
    }
}
