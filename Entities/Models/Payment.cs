using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public enum Status
    {
        Ok,
        NotPaid
    }
    public class Payment
    {
        public Payment()
        {
            this.Orders = new List<Order>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public double Amount { get; set; }
        public Status PayStatus { get; set; }
        public DateTime PaidAt { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
