using System;
using System.Collections.Generic;
using System.Text;
using static Entities.Models.SmartBox;

namespace Entities.ViewModels.ContainerViewModels
{
    public class BoxDataResponse
    {
        public string SensorName { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        //public Guid Id { get; set; }
        //public string Name { get; set; }
        //public string IsOpenedBox { get; set; }
        //public string BoxState { get; set; }
        //public string IsOpenedDoor { get; set; }
        //public string Weight { get; set; }
        //public string Light { get; set; }
        //public string Temperature { get; set; }
        //public string Wetness { get; set; }
        //public string BatteryPower { get; set; }
        //public string SignalLevel { get; set; }
        //public DateTime Date { get; set; }
        //public double? Longitude { get; set; }
        //public double? Latitude { get; set; }
    }
}
