using Entities.ViewModels.ImitatorViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels.ImitatorViewModels
{
    public class UserModel: BaseModel
    {
        [Required]
        public string UserFIO { get; set; }
        public string Description { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsAllowed { get; set; }
    }
}
