using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class ErrorResponse: BaseResponseObject
    {
        public List<string> Errors { get; set; }
    }
}
