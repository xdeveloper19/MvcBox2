using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class Driver
    {
        public Driver()
        {
            this.Tasks = new List<Task>();
            this.DriverHasBoxes = new List<DriverHasBox>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(45)]
        public string Number { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int BoxCount { get; set; }
        public int BoxState { get; set; }
        public bool IsBusy { get; set; }
        public string AccountId { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<DriverHasBox> DriverHasBoxes { get; set; }
    }
}
