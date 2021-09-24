using Entities.Configuration;
using Entities.Interfaces;
using Entities.ViewModels.ImitatorViewModels;
using Microsoft.Extensions.Options;
using MvcBox.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MvcBox.Services
{
    public class LocationService: ILocationService
    {
        private readonly ILocationRepository _gpsRepository;
        private readonly IAppLogger<LocationService> _logger;
        private readonly AppSettings _appSettings;

        public LocationService(ILocationRepository devRepository,
            IAppLogger<LocationService> logger, IOptions<AppSettings> appSettings)
        {
            _gpsRepository = devRepository ?? throw new ArgumentNullException(nameof(devRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appSettings = appSettings.Value;
        }

        public async Task UpdateCoordinates(PositionModel model)
        {
            try
            {
                await _gpsRepository.UpdateCoordinates(model);
                _logger.LogInformation($"Entity successfully added - BaseProject");
            }
            catch (Exception exp)
            {
                _logger.LogWarning("Сервисы создания GPS не доступны.", exp);
                throw;
            }
        }
    }
}
