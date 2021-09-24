using Entities.ViewModels.ImitatorViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.ImitatorViewModels
{
    public class DeviceShortModel: BaseModel
    {
        public string IMEI { get; set; }
        public bool HasPhotoRequest { get; set; }
    }
}
