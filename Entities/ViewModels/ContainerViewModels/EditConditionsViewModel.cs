using System;
using System.Collections.Generic;
using System.Text;
using static Entities.Models.SmartBox;

namespace Entities.ViewModels.ContainerViewModels
{
    public class EditConditionsViewModel
    {
        public Guid Id { get; set; }
        public bool IsOpenedDoor { get; set; }
        public ContainerState State { get; set; }
        public bool IsOpenedBox { get; set; }
    }
}
