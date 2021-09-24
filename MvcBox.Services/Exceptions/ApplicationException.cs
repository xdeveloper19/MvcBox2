﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MvcBox.Services.Exceptions
{
    public class ApplicationException: Exception
    {
        internal ApplicationException(string businessMessage)
              : base(businessMessage)
        {
        }

        internal ApplicationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
