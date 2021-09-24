using Entities.Models.Base;
using Entities.ViewModels.ImitatorViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models.Imitator
{
    public class Position: EntityBase
    {
        public Guid DeviceId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string CurrentDate { get; set; }

        public static Position Create(PositionModel model)
        {
            var pos = new Position()
            {
                Latitude = model.Latitude,
                DeviceId = model.DeviceId,
                CurrentDate = model.CurrentDate,
                Longitude = model.Longitude
            };
            return pos;
        }
    }
}
