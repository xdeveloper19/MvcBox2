using Entities.Configuration;
using Entities.Interfaces;
using Entities.ViewModels.ImitatorViewModels;
using Entities.ViewModels.ImitatorViewModels.Base;
using Microsoft.Extensions.Options;
using MvcBox.Services.Interfaces;
using MvcBox.Services.Mapper;
using System;
using System.Threading.Tasks;

namespace MvcBox.Services
{
    public class DeviceService: IDeviceService
    {
        private readonly IDeviceRepository _devRepository;
        private readonly IAppLogger<DeviceService> _logger;
        private readonly AppSettings _appSettings;

        public DeviceService(IDeviceRepository devRepository,
            IAppLogger<DeviceService> logger, IOptions<AppSettings> appSettings)
        {
            _devRepository = devRepository ?? throw new ArgumentNullException(nameof(devRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Регистрация мобильного устройства.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="owId"></param>
        /// <returns></returns>
        public async Task<DeviceModel> RegisterDevice(DeviceModel model, Guid owId)
        {
            try
            {
                // validation
                if (string.IsNullOrWhiteSpace(model.IMEI) || string.IsNullOrWhiteSpace(model.ModelName))
                    throw new Exception("IMEI (и)или наименование модели отсутствуют.");

                var mappedEntity = await _devRepository.CreateDevice(model, owId);
                _logger.LogInformation($"Entity successfully added - BaseProject");
                var newMappedEntity = ObjectMapper.Mapper.Map<DeviceModel>(mappedEntity);
                newMappedEntity.SuccessInfo = "Мобильное устройство успешно зарегистрировано.";
                return newMappedEntity;
            }
            catch (Exception exp)
            {
                _logger.LogWarning("При создании устройства возникла ошибка", exp);
                return BaseModelUtilities<DeviceModel>.ErrorFormat(exp);
            }
        }

        /// <summary>
        /// Поиск запросов на получение фото.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeviceShortModel> SearchPhotoRequest(Guid id)
        {
            try
            {
                var mappedEntity = await _devRepository.GetDevice(id);

                var newMappedEntity = ObjectMapper.Mapper.Map<DeviceShortModel>(mappedEntity);
                newMappedEntity.SuccessInfo = (newMappedEntity.HasPhotoRequest == true)?
                    "Запрос успешно получен!":"Запросов от сторонних пользователей не имеется.";

                mappedEntity.HasPhotoRequest = false;
                await _devRepository.UpdateAsync(mappedEntity);
                return newMappedEntity;
            }
            catch (Exception exp)
            {
                _logger.LogWarning("При поиске запросов возникла ошибка: ", exp);
                return BaseModelUtilities<DeviceShortModel>.ErrorFormat(exp);
            }
        }

        /// <summary>
        /// Добавить запрос на получение фото
        /// </summary>
        /// <param name="IMEI"></param>
        /// <returns></returns>
        public async Task UpdatePhotoRequest(string IMEI)
        {
            try
            {
                var data = _devRepository.GetDevice(IMEI);
                var dev = data.Result;

                if (dev.OwnerId == Guid.Empty || dev == null)
                {
                    new Exception("Невозможно добавить запрос на получение фото.");
                }

                dev.HasPhotoRequest = true;
                await _devRepository.UpdateAsync(dev);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
