using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Rate
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double HRate { get; set; }
        
    }
}
