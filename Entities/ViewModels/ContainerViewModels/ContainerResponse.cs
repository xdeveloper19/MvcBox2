using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.ContainerViewModels
{
    public class ContainerResponse: BaseResponseObject
    {
        public Guid SmartBoxId { get; set; }
        public string Name { get; set; }
    }
}
