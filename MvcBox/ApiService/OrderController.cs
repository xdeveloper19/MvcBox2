using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MvcBox.ApiService
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //private readonly SmartBoxContext _boxContext;
        //private readonly UserManager<User> _userManager;
        //public OrderController(SmartBoxContext boxContext, UserManager<User> userManager)
        //{
        //    _boxContext = boxContext;
        //    _userManager = userManager;
        //}

        //[HttpPost]
        //[Route("CreateOrder")]
        //public async Task<ServiceResponseObject<OrderResponse>> CreateOrder(string userId)
        //{
        //    OrderMethods BoxData = new OrderMethods(_boxContext, _userManager);
        //    var Result = await BoxData.CreateOrder(userId);
        //    return Result;
        //}

        //[HttpGet]
        //[Route("GetTask")]
        //public async Task<ServiceResponseObject<TaskResponse>> GetTask(Guid driverId)
        //{
        //    OrderMethods BoxData = new OrderMethods(_boxContext, _userManager);
        //    var Result = await BoxData.GetTask(driverId);
        //    return Result;
        //}

        //[HttpPost]
        //[Route("SendAccess")]
        //public async Task<ServiceResponseObject<BaseResponseObject>> SendAccess(Guid orderId)
        //{
        //    OrderMethods BoxData = new OrderMethods(_boxContext, _userManager);
        //    var Result = await BoxData.SendAccess(orderId);
        //    return Result;
        //}

        //[HttpPost]
        //[Route("CreatePayment")]
        //public async Task<ServiceResponseObject<BaseResponseObject>> CreatePayment(Guid orderId)
        //{
        //    OrderMethods BoxData = new OrderMethods(_boxContext, _userManager);
        //    var Result = await BoxData.CreatePayment(orderId);
        //    return Result;
        //}

        //[HttpGet]
        //[Route("CheckPayment")]
        //public async Task<ServiceResponseObject<BaseResponseObject>> CheckPayment(Guid orderId)
        //{
        //    OrderMethods BoxData = new OrderMethods(_boxContext, _userManager);
        //    var Result = await BoxData.CheckPayment(orderId);
        //    return Result;
        //}

        //[HttpPost]
        //[Route("CreateDeliveryRequest")]
        //public async Task<ServiceResponseObject<BaseResponseObject>> CreateDeliveryRequest(Guid orderId)
        //{
        //    OrderMethods BoxData = new OrderMethods(_boxContext, _userManager);
        //    var Result = await BoxData.CreateDeliveryRequest(orderId);
        //    return Result;
        //}

        //[HttpPut]
        //[Route("CompleteOrder")]
        //public async Task<ServiceResponseObject<BaseResponseObject>> CompleteOrder(Guid orderId)
        //{
        //    OrderMethods BoxData = new OrderMethods(_boxContext, _userManager);
        //    var Result = await BoxData.CompleteOrder(orderId);
        //    return Result;
        //}
    }
}
