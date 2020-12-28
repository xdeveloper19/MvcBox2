using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class OrderStage
    {
        public OrderStage()
        {
            this.Orders = new List<Order>();
            this.OrderStageLogs = new List<OrderStageLog>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(45)]
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<OrderStageLog> OrderStageLogs { get; set; }
    }
}
