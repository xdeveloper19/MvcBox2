using Entities.Models.Imitator;
using Entities.ViewModels.ImitatorViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IDeviceRepository: IRepository<Device>
    {
        //Task<User> GetUserWithDailyRowsAsync(Guid userId);
        Task<Device> CreateDevice(DeviceModel model, Guid owId);
        Task<Device> GetDevice(Guid id);
        Task<Device> GetDevice(string IMEI);
    }
}
