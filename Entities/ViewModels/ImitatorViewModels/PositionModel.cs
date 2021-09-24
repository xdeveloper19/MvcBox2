using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.ImitatorViewModels
{
    public class PositionModel
    {
        public Guid DeviceId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string CurrentDate { get; set; }
    }
}
