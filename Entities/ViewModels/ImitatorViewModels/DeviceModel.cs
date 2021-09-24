using Entities.ViewModels.ImitatorViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels.ImitatorViewModels
{
    public class DeviceModel: BaseModel
    {
        [Required]
        public string ModelName { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
        [Required]
        public string IMEI { get; set; }
        public string Idiom { get; set; }
    }
}
