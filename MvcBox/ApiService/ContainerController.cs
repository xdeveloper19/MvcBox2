using Entities.Context;
using Entities.Models;
using Entities.Repository;
using Entities.ViewModels;
using Entities.ViewModels.ContainerViewModels;
using Entities.ViewModels.OrderViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcBox.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBox.ApiService
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly SmartBoxContext _boxContext;
        private readonly UserManager<User> _userManager;
        private readonly IDeviceService _devService;

        public ContainerController(SmartBoxContext boxContext, UserManager<User> userManager, IDeviceService dev)
        {
            _boxContext = boxContext;
            _userManager = userManager;
            _devService = dev ?? throw new ArgumentNullException(nameof(dev));
        }

        /// <summary>
        /// Создает вирутальный контейнер
        /// </summary>
        /// <param name="name">IMEI устройства</param>
        /// <returns>Успех или нет</returns>
        [HttpPost]
        [Route("Create")]
        public async Task<ServiceResponseObject<ContainerResponse>> Create(string name)
        {
            ContainerMethods BoxData = new ContainerMethods(_boxContext);
            var Result = await BoxData.Create(name);

            try
            {
                await _devService.UpdatePhotoRequest(name);
                Result.Message = "Успешно.";
                Result.Status = ResponseResult.OK;
                return Result;
            }
            catch (Exception ex)
            {
                Result.Message = ex.Message;
                Result.Status = ResponseResult.Error;
                return Result;
            }
        }

        [HttpGet]
        [Route("SearchCommandPhoto")]
        public async Task<ServiceResponseObject<BaseResponseObject>> SearchCommandPhoto(string name)
        {
            ContainerMethods BoxData = new ContainerMethods(_boxContext);
            var Result = await BoxData.SearchCommandPhoto(name);
            return Result;
        }


        [HttpPost]
        [Route("EditSensors")]
        public async Task<ServiceResponseObject<BaseResponseObject>> EditSensors(EditBoxViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    ContainerMethods BoxData = new ContainerMethods(_boxContext);
            //    var Result = await BoxData.EditSensors(model);
            //    return Result;
            //}
            ContainerMethods BoxData = new ContainerMethods(_boxContext);
            var Result = await BoxData.EditSensors(model);
            return Result;
            //ServiceResponseObject<BaseResponseObject> response = new ServiceResponseObject<BaseResponseObject>();
            //response.Status = ResponseResult.Error;

            //List<string> errors = ModelState.Values.Aggregate(
            //   new List<string>(),
            //   (a, c) =>
            //   {
            //       a.AddRange(c.Errors.Select(r => r.ErrorMessage));
            //       return a;
            //   },
            //   a => a
            //);

            //response.Message = errors[0];
            //return response;
        }

        [HttpPost]
        [Route("SetContainerLocation")]
        public async Task<ServiceResponseObject<BaseResponseObject>> SetContainerLocation(LocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                ContainerMethods BoxData = new ContainerMethods(_boxContext);
                var Result = await BoxData.SetContainerLocation(model);
                return Result;
            }

            ServiceResponseObject<BaseResponseObject> response = new ServiceResponseObject<BaseResponseObject>();
            response.Status = ResponseResult.Error;

            List<string> errors = ModelState.Values.Aggregate(
               new List<string>(),
               (a, c) =>
               {
                   a.AddRange(c.Errors.Select(r => r.ErrorMessage));
                   return a;
               },
               a => a
            );

            response.Message = errors[0];
            return response;
        }

        [HttpGet]
        [Route("GetBox")]
        public async Task<ServiceResponseObject<ListResponse<BoxDataResponse>>> GetBox(Guid id) /*Guid driverId, Guid orderId*/
        {
            ContainerMethods BoxData = new ContainerMethods(_boxContext);
            var Result = await BoxData.GetBox(id);
            //if (Result.Status == ResponseResult.OK)
            //{
            //    if (SearchDriver(driverId))
            //    {
            //        OrderHasBox order = new OrderHasBox
            //        {
            //            BoxId = id,
            //            IsBusy = true,
            //            OrderId = orderId
            //        };

            //        DriverHasBox hasBox = new DriverHasBox
            //        {
            //            BoxId = id,
            //            DriverId = driverId
            //        };
            //        _boxContext.DriverHasBoxes.Add(hasBox);
            //        _boxContext.OrderHasBoxes.Add(order);
            //        _boxContext.SaveChanges();
            //        Result.Message += " Контейнер привязан.";
            //    }
            //}
            return Result;
        }

        #region Obsolete

        [HttpGet]
        [Route("GetBoxesByName")]
        public async Task<ServiceResponseObject<ListResponse<ContainerResponse>>> GetBoxesByName(string name)
        {
            ContainerMethods BoxData = new ContainerMethods(_boxContext);
            var Result = await BoxData.GetBoxesByName(name);
            return Result;
        }

        //[HttpGet]
        //[Route("GetBoxForUser")]
        //public async Task<ServiceResponseObject<BoxDataResponse>> GetBoxForUser(string userId)
        //{
        //    ContainerMethods BoxData = new ContainerMethods(_boxContext);
        //    var access = await _boxContext.UserHasAccesses.Where(p => p.UserId == userId).FirstOrDefaultAsync();
        //    if (access != null)
        //    {
        //        var Result = await BoxData.GetBox(access.BoxId);
        //        Result.Message += " Оплатите заказ.";
        //        return Result;
        //    }
        //    ServiceResponseObject<BoxDataResponse> response = new ServiceResponseObject<BoxDataResponse>();
        //    response.Message = "Нет доступа.";
        //    response.Status = ResponseResult.Error;
        //    return response;
        //}

        [HttpGet]
        [Route("GetRandomBox")]
        public async Task<ServiceResponseObject<GetBoxIdResponse>> GetRandomBox()
        {
            ContainerMethods BoxData = new ContainerMethods(_boxContext);
            var Result = await BoxData.GetRandomBox();
            return Result;
        }

        [HttpPatch]
        [Route("GetImitatorSensors")]
        public async Task<ActionResult> GetImitatorSensors(string id)
        {
            //Proxy Server
            ContainerMethods BoxData = new ContainerMethods(_boxContext);
            var Result = await BoxData.GetImitatorSensors(id);
            if (Result.Status == ResponseResult.Error)
                return BadRequest(Result);
            else
                return Ok(Result.ResponseData);
        }

        [HttpPatch]
        [Route("TestSensors")]
        public async Task<ActionResult<ServiceResponseObject<BaseResponseObject>>> TestSensors(string id, string temp, string light, string humidity)
        {
            ContainerMethods BoxData = new ContainerMethods(_boxContext);
            var Result = await BoxData.TestSensors(id, temp, light, humidity);
            if (Result.Status == ResponseResult.Error)
                return BadRequest(Result);
            else
                return Ok(Result);
        }

        [HttpGet]
        [Route("GetBoxesLocation")]
        public async Task<JsonResult> GetBoxesLocation()
        {
            ContainerMethods BoxData = new ContainerMethods(_boxContext);
            JsonResult result = null;
            var data = await BoxData.GetBoxesLocation();
            var boxes = data.ResponseData.Objects;
            result = new JsonResult(boxes);
            return result;
        }

        [HttpGet]
        [Route("PriceComputation")]
        public ServiceResponseObject<ComputationResponse> PriceComputation(PriceComputationViewModel model)
        {
            ServiceResponseObject<ComputationResponse> response = new ServiceResponseObject<ComputationResponse>();
            if (model.CargeType == "Выбор" || model.DangerClassType == "Выбор")
            {
                ModelState.AddModelError(string.Empty, "Укажите характер груза или класс опасности");
            }

            if (ModelState.IsValid)
            {
                OrderMethods BoxData = new OrderMethods(_boxContext, _userManager);
                var price = BoxData.PriceComputation(model);
                response.Message = "Успешно!";
                response.ResponseData = new ComputationResponse { Price = price };
                response.Status = ResponseResult.OK;
                return response;
            }
            response.Status = ResponseResult.Error;
            List<string> errors = ModelState.Values.Aggregate(
               new List<string>(),
               (a, c) =>
               {
                   a.AddRange(c.Errors.Select(r => r.ErrorMessage));
                   return a;
               },
               a => a
            );

            response.ResponseData = new ComputationResponse();
            response.Message = errors[0];
            return response;
        }
        
        // GET: api/Container
        [HttpGet]
        [Route("Get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        private bool SearchDriver(Guid driverId)
        {
            try
            {
                //var driver = _boxContext.Drivers.Find(driverId);
                //if (driver == null)
                //    return false;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
