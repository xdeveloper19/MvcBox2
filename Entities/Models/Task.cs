using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Task
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }
        public string Text { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [MaxLength(255)]
        public string WayPoints { get; set; }
        public Guid OrderId { get; set; }
        public Guid DriverId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AbortedBy { get; set; }
        public DateTime AbortedAt { get; set; }
        public DateTime DoneAt { get; set; }
        public bool IsCompleted { get; set; }
        [MaxLength(45)]
        public string TaskType { get; set; }

        [MaxLength(45)]
        public string TaskPriority { get; set; }

    }
}
