using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class ListResponse<T>: BaseResponseObject
    {
        public ListResponse()
        {
            this.Objects = new List<T>();
        }
        public List<T> Objects { get; set; }
    }
}
