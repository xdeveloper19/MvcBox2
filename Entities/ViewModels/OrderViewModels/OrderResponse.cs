using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.OrderViewModels
{
    public class OrderResponse: BaseResponseObject
    {
        public Guid OrderId { get; set; }
    }
}
