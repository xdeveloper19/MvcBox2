using System;

namespace Entities.Models
{
    public class Media
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Path { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid BoxId { get; set; }
    }
}
