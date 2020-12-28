using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Order
    {
        public Order()
        {
            this.Tasks = new List<Task>();
            this.OrderHasBoxes = new List<OrderHasBox>();
            this.OrderStageLogs = new List<OrderStageLog>();
            this.UserHasOrders = new List<UserHasOrder>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string InceptionAdress { get; set; }
        public double InceptionLatitude { get; set; }
        public double InceptionLongitude { get; set; }
        public string DestinationAddress { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }
        public string ShipmentTime { get; set; }
        public string LoadMethod { get; set; }
        public string CargoType { get; set; }
        public string CargoClass { get; set; }
        public double Distance { get; set; }
        public double Insurance { get; set; }
        public Guid PaymentId { get; set; }
        public Guid OrderStageId { get; set; }
        public ICollection<OrderHasBox> OrderHasBoxes{ get; set; }
        public ICollection<OrderStageLog> OrderStageLogs{ get; set; }
        public ICollection<UserHasOrder> UserHasOrders { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
