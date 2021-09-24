using Entities.Models.Base;
using Entities.ViewModels.ImitatorViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models.Imitator
{
    public class Device: EntityBase
    {
        public Device()
        {
            this.Positions = new List<Position>();
        }
        public Guid OwnerId { get; set; }
        public string ModelName { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
        public string IMEI { get; set; }
        public string Idiom { get; set; }

        public bool HasPhotoRequest { get; set; }

        public static Device Create(DeviceModel model, Guid owId)
        {
            var dev = new Device
            {
                HasPhotoRequest = false,
                Idiom = model.Idiom,
                IMEI = model.IMEI,
                Manufacturer = model.Manufacturer,
                ModelName = model.ModelName,
                Name = model.Name,
                OwnerId = owId,
                Platform = model.Platform,
                Version = model.Version
            };
            return dev;
        }

        public List<Position> Positions { get; set; }
    }
}
