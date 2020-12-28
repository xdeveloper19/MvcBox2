using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class VariableGroup
    {
        public VariableGroup()
        {
            this.VariableGroupes = new List<VariableGroup>();
            this.Variables = new List<Variable>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(45)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [MaxLength(45)]
        public string Function { get; set; }
        public Guid VariableGroupId { get; set; }
        public ICollection<VariableGroup> VariableGroupes { get; set; }
        public ICollection<Variable> Variables { get; set; }
    }
}
