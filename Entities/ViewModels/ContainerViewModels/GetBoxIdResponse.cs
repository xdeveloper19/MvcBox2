using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.ContainerViewModels
{
    public class GetBoxIdResponse: BaseResponseObject
    {
        public string BoxId { get; set; }
        public string Name { get; set; }
    }
}
