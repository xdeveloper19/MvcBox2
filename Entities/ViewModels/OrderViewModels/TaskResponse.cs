using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.OrderViewModels
{
    public class TaskResponse: BaseResponseObject
    {
        public Guid OrderId { get; set; }
        public string TaskType { get; set; }
    }
}
