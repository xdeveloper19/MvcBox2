using Entities.Context;
using Entities.Interfaces;
using Entities.Models.Imitator;
using Entities.Repository.Base;
using Entities.ViewModels.ImitatorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository.Imitator
{
    public class DeviceRepository: Repository<Device>, IDeviceRepository
    {
        public DeviceRepository(ImitatorContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Добавление нового мобильного устройства в базу данных.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Device> CreateDevice(DeviceModel model, Guid owId)
        {
            var GetData = GetAsyncIQueryable(m => m.IMEI == model.IMEI);
            if (GetData.Count() != 0)
            {
                throw new Exception("Мобильное устройство с таким IMEI уже существует.");
            }
            var mappedEntity = Device.Create(model, owId);
            var newEntity = await AddAsync(mappedEntity);
            return newEntity;
        }

        /// <summary>
        /// Получение устройства по уникальному номеру.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Device> GetDevice(Guid id)
        {
            var category = (await GetAsync(m => m.Id == id)).FirstOrDefault();
            if (category == null)
            {
                throw new Exception("Устройство не найдено.");
            }
            return category;
        }

        /// <summary>
        /// Получение устройства по уникальному IMEI.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Device> GetDevice(string IMEI)
        {
            var category = (await GetAsync(m => m.IMEI == IMEI)).FirstOrDefault();
            if (category == null)
            {
                throw new Exception("Устройство не найдено.");
            }
            return category;
        }
    }
}
