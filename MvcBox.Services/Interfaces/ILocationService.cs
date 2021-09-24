using Entities.ViewModels.ImitatorViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MvcBox.Services.Interfaces
{
    public interface ILocationService
    {
        Task UpdateCoordinates(PositionModel model);
    }
}
