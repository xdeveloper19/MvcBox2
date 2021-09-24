using Entities.Context;
using Entities.Interfaces;
using Entities.Models.Imitator;
using Entities.Repository.Base;
using Entities.ViewModels.ImitatorViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository.Imitator
{
    public class LocationRepository : Repository<Position>, ILocationRepository
    {
        public LocationRepository(ImitatorContext dbContext) : base(dbContext)
        {
        }

        public async Task UpdateCoordinates(PositionModel model)
        {
            await GetDeviceAsync(model.DeviceId);
            var mappedEntity = Position.Create(model);
            await AddAsync(mappedEntity);
        }

        private async Task<Device> GetDeviceAsync(Guid id)
        {
            var GetData = await _dbContext.Devices.FindAsync(id);
            if (GetData == null)
            {
                throw new Exception("Мобильное устройство не обнаружено.");
            }
            return GetData;
        }
    }
}
