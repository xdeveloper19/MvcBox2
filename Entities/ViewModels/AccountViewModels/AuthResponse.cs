using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.AccountViewModels
{
    public class AuthResponse: BaseResponseObject
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid DriverId { get; set; }
    }
}
