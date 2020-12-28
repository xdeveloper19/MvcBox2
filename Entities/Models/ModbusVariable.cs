using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class ModbusVariable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Function { get; set; }
        public int Sign { get; set; }
        public int Address { get; set; }
        [MaxLength(45)]
        public string Type { get; set; }
        public int Size { get; set; }
        public bool Trackable { get; set; }
        public int PollingTime { get; set; }
        public Guid VariableId { get; set; }
    }
}
