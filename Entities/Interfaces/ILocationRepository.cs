using Entities.Models.Imitator;
using Entities.ViewModels.ImitatorViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface ILocationRepository: IRepository<Position>
    {
        Task UpdateCoordinates(PositionModel model);
    }
}
