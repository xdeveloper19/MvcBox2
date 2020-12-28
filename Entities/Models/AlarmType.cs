using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class AlarmType
    {
        public AlarmType()
        {
            this.Alarms = new List<Alarm>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(255)]
        public string Name { get; set; }
        public ICollection<Alarm> Alarms { get; set; }
    }
}
