using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class Variable
    {
        public Variable()
        {
            this.ModbusVariables = new List<ModbusVariable>();
            this.VariableNotifies = new List<VariableNotify>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Value { get; set; }
        [MaxLength(512)]
        public string DefaultValue { get; set; }
        public string Description { get; set; }
        public bool Modifiable { get; set; }
        public Guid VariableGroupId { get; set; }
        public Guid BoxId { get; set; }
        public ICollection<ModbusVariable> ModbusVariables { get; set; }
        public ICollection<VariableNotify> VariableNotifies { get; set; }
    }
}
