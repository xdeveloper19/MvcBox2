using Entities.ViewModels.ImitatorViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MvcBox.Services.Interfaces
{
    public interface IDeviceService
    {
        //Task<User> GetUserWithDailyRowsAsync(Guid userId);
        Task<DeviceModel> RegisterDevice(DeviceModel model, Guid owId);
        Task<DeviceShortModel> SearchPhotoRequest(Guid id);
        Task UpdatePhotoRequest(string IMEI);
    }
}
