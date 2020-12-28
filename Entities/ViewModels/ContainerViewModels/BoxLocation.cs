using System;
using System.Collections.Generic;
using System.Text;
using static Entities.Models.SmartBox;

namespace Entities.ViewModels.ContainerViewModels
{
    public class BoxLocation
    {
        public Guid SmartBoxId { get; set; }
        public string Name { get; set; }
        public bool IsOpenedBox { get; set; }
        public ContainerState BoxState { get; set; }
        public bool IsOpenedDoor { get; set; }
        public double Weight { get; set; }
        public int Light { get; set; }
        public string Code { get; set; }
        public double Temperature { get; set; }
        public double Wetness { get; set; }
        public double BatteryPower { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}
