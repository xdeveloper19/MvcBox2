using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Entities.Models.SmartBox;

namespace Entities.ViewModels.ContainerViewModels
{
    public class EditBoxViewModel
    {
        public string Id { get; set; }
        //public string Weight { get; set; }
        //public string Light { get; set; }
        //public string Temperature { get; set; }
        //public string SignalLevel { get; set; }
        public DateTime Date { get;set; }
        //public string Wetness { get; set; }
        //public string BatteryPower { get; set; }
        public Dictionary<string,string> Sensors { get; set; }
    }
}
