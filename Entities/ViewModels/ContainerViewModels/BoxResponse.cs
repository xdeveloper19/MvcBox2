using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.ContainerViewModels
{
    public class BoxResponse: BaseResponseObject
    {
        public BoxResponse()
        {
            this.status = new Status();
        }
        public Status status { get; set; }
    }
}
