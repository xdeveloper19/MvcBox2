using Entities.Context;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.OrderViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public class OrderMethods
    {
        private readonly SmartBoxContext _boxContext;
        private readonly UserManager<User> _userManager;
        public OrderMethods(SmartBoxContext boxContext, UserManager<User> userManager)
        {
            _boxContext = boxContext;
            _userManager = userManager;
            //this.Rates = new List<Rate>();
            //Rates.Add(new Rate { Name = "Способ погрузки" });
            //Rates.Add(new Rate { Name = "Класс опасности" });
            //Rates.Add(new Rate { Name = "Страховой коэффициент" });

            //Rates[0].RateTypes.Add(new RateType { Name = "Тарно-штучные", Price = 0.01, RateId = Rates[0].Id });
            //Rates[0].RateTypes.Add(new RateType { Name = "Жидкие", Price = 0.011, RateId = Rates[0].Id });
            //Rates[0].RateTypes.Add(new RateType { Name = "Насыпные", Price = 0.012, RateId = Rates[0].Id });

            //Rates[1].RateTypes.Add(new RateType { Name = "0_класс", Index = 1, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "1_класс", Index = 1.25, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "2_класс", Index = 1.29, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "3_класс", Index = 1.4, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "4.1_класс", Index = 1.5, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "4.2_класс", Index = 1.8, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "4.3_класс", Index = 2.2, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "5.1_класс", Index = 2.1, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "5.2_класс", Index = 1.6, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "6.1_класс", Index = 1.25, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "6.2_класс", Index = 2.7, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "7_класс", Index = 1.34, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "8_класс", Index = 1.89, RateId = Rates[1].Id });
            //Rates[1].RateTypes.Add(new RateType { Name = "9_класс", Index = 1.1, RateId = Rates[1].Id });

            //Rates[2].RateTypes.Add(new RateType { Name = "Страхование", Index = 0.0012, RateId = Rates[2].Id });
        }

        /// <summary>
        /// Вычисление предварительной цены заказа
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public double PriceComputation(PriceComputationViewModel model)
        {
            //try
            //{
            //    var dangerClass = Rates[1].RateTypes.Find(f => f.Name == model.DangerClassType);
            //    var carge = Rates[0].RateTypes.Find(f => f.Name == model.CargeType) ;

            //    var price = model.Length * model.Weight * (carge.Price) * (dangerClass.Index);
            //    if (model.IsInsured)
            //    {
            //        price += (Rates[2].RateTypes[0].Index) * model.CargeValue;
            //    }

            //    //var result = Math.Ceiling(price);
            //    return price;
            //}
            //catch (Exception)
            //{
            //    return 0.00;
            //}
            return 0.00;
        }


        /// <summary>
        /// Формирование заявки на перевозку груза
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public async Task<ServiceResponseObject<OrderResponse>> CreateOrder(string userId)
        //{
        //    ServiceResponseObject<OrderResponse> response = new ServiceResponseObject<OrderResponse>();
        //    var driver = await _boxContext.Drivers.Where(p => p.IsBusy == false).FirstOrDefaultAsync();
        //    var client = await _userManager.FindByIdAsync(userId);

        //    if (driver == null || client == null)
        //    {
        //        response.Status = ResponseResult.Error;
        //        response.Message = "Свободных водителей нет.";
        //        return response;
        //    }

        //    if (userId == driver.AccountId)
        //    {
        //        response.Status = ResponseResult.Error;
        //        response.Message = "Водитель не может оформить заказ.";
        //        return response;
        //    }


        //    Payment payment = new Payment
        //    {
        //        Amount = 1000,
        //        PayStatus = Status.NotPaid
        //    };
        //    _boxContext.Payments.Add(payment);
        //    _boxContext.SaveChanges();

        //    Order order = new Order();
        //    order.PaymentId = payment.Id;
        //    order.OrderStageId = await _boxContext.OrderStages.Where(p => p.Name == "Заявка").Select(s => s.Id).FirstOrDefaultAsync();
        //    _boxContext.Orders.Add(order);
        //    _boxContext.SaveChanges();

        //    Models.Task task = new Models.Task
        //    {
        //        CreatedAt = DateTime.Now,
        //        DriverId = driver.Id,
        //        OrderId = order.Id,
        //        TaskType = "1. Привести пустой контейнер и забрать груз."
        //    };
        //    _boxContext.Tasks.Add(task);
        //    _boxContext.SaveChanges();

        //    UserHasOrder userWithOrder = new UserHasOrder
        //    {
        //        OrderId = order.Id,
        //        UserId = client.Id
        //    };
        //    _boxContext.UserHasOrders.Add(userWithOrder);
        //    _boxContext.SaveChanges();

        //    driver.BoxState = 2;
        //    _boxContext.Update(driver);
        //    _boxContext.SaveChanges();

        //    response.ResponseData = new OrderResponse
        //    {
        //        OrderId = order.Id
        //    };
        //    response.Status = ResponseResult.OK;
        //    response.Message = "Ваш заказ готовится, пожалуйста, подождите.";
        //    return response;
        //}


        /// <summary>
        /// Получение списка задач водителя
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        //public async Task<ServiceResponseObject<TaskResponse>> GetTask(Guid driverId)
        //{
        //    ServiceResponseObject<TaskResponse> Data = new ServiceResponseObject<TaskResponse>();
        //    var driver = await _boxContext.Drivers.FindAsync(driverId);

        //    _boxContext.Entry(driver)
        //    .Collection(c => c.Tasks)
        //    .Load();

        //    var tasks = driver.Tasks;
        //    if (tasks.Count == 0 || tasks == null)
        //    {
        //        Data.Status = ResponseResult.Error;
        //        Data.Message = "Задач нет.";
        //        return Data;
        //    }

        //    Data.ResponseData = await _boxContext.Tasks.Where(t => t.IsCompleted == false).Select(s =>
        //        new TaskResponse
        //        {
        //            OrderId = s.OrderId,
        //            TaskType = s.TaskType
        //        }).FirstOrDefaultAsync();

        //    Data.Message = "Новая задача!";
        //    Data.Status = ResponseResult.OK;
        //    return Data;
        //}


        /// <summary>
        /// Передача доступа к контейнеру
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        //public async Task<ServiceResponseObject<BaseResponseObject>> SendAccess(Guid orderId)
        //{
        //    ServiceResponseObject<BaseResponseObject> response = new ServiceResponseObject<BaseResponseObject>();
        //    try
        //    {
        //        var order = await _boxContext.UserHasOrders.Where(p => p.OrderId == orderId).FirstOrDefaultAsync();
        //        var box = await _boxContext.OrderHasBoxes.Where(p => p.OrderId == orderId).FirstOrDefaultAsync();
        //        var task = await _boxContext.Tasks.Where(p => p.OrderId == orderId).FirstOrDefaultAsync();

        //        task.IsCompleted = true;
        //        task.DoneAt = DateTime.Now;
        //        _boxContext.Update(task);
        //        _boxContext.SaveChanges();

        //        UserHasAccess access = new UserHasAccess
        //        {
        //            BoxId = box.BoxId,
        //            UserId = order.UserId
        //        };
        //        _boxContext.UserHasAccesses.Add(access);
        //        _boxContext.SaveChanges();
        //        response.Message = "Доступ отправлен. Ожидается оплата.";
        //        response.Status = ResponseResult.OK;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = ResponseResult.Error;
        //        response.Message = "Что-то пошло не так... " + ex.Message;
        //        return response;
        //    }
        //}


        /// <summary>
        /// Совершение платежа
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        //public async Task<ServiceResponseObject<BaseResponseObject>> CreatePayment(Guid orderId)
        //{
        //    ServiceResponseObject<BaseResponseObject> response = new ServiceResponseObject<BaseResponseObject>();
        //    var order = await _boxContext.Orders.FindAsync(orderId);

        //    if (order != null)
        //    {
        //        var payment = await _boxContext.Payments.FindAsync(order.PaymentId);
        //        payment.PayStatus = Status.Ok;
        //        payment.PaidAt = DateTime.Now;
        //        response.Status = ResponseResult.OK;
        //        response.Message = "Оплата успешно произведена.";
        //        return response;
        //    }
        //    response.Status = ResponseResult.Error;
        //    response.Message = "Заказ не найден.";
        //    return response;
        //}


        /// <summary>
        /// Проверка наличия платежа
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        //public async Task<ServiceResponseObject<BaseResponseObject>> CheckPayment(Guid orderId)
        //{
        //    ServiceResponseObject<BaseResponseObject> response = new ServiceResponseObject<BaseResponseObject>();
        //    var order = await _boxContext.Orders.FindAsync(orderId);

        //    if (order != null)
        //    {
        //        var payment = await _boxContext.Payments.FindAsync(order.PaymentId);
        //        if (payment.PayStatus == Status.Ok)
        //        {
        //            response.Message = "Оплата успешно произведена.";
        //            response.Status = ResponseResult.OK;
        //            return response;
        //        }
        //        response.Message = "Клиент ещё не оплатил заказ.";
        //        response.Status = ResponseResult.Error;
        //        return response;
        //    }

        //    response.Status = ResponseResult.Error;
        //    response.Message = "Заказ не найден.";
        //    return response;
        //}


        /// <summary>
        /// Передача загруженного контейнера в службу доставки
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        //public async Task<ServiceResponseObject<BaseResponseObject>> CreateDeliveryRequest(Guid orderId)
        //{
        //    ServiceResponseObject<BaseResponseObject> response = new ServiceResponseObject<BaseResponseObject>();
        //    try
        //    {
        //        var order = await _boxContext.Orders.FindAsync(orderId);
        //        var hasBox = await _boxContext.OrderHasBoxes.Where(p => p.OrderId == orderId).FirstOrDefaultAsync();
        //        var driverId = await _boxContext.DriverHasBoxes.Where(p => p.BoxId == hasBox.BoxId).Select(s => s.DriverId).FirstOrDefaultAsync();

        //        Models.Task task = new Models.Task
        //        {
        //            CreatedAt = DateTime.Now,
        //            DriverId = driverId,
        //            OrderId = order.Id,
        //            TaskType = "2. Полный контейнер готов к отгрузке."
        //        };

        //        _boxContext.Tasks.Add(task);
        //        _boxContext.SaveChanges();
        //        response.Message = "Контейнер передан в службу доставки.";
        //        response.Status = ResponseResult.OK;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = "Что-то пошло не так..." + ex.Message;
        //        response.Status = ResponseResult.Error;
        //        throw;
        //    }
        //}


        /// <summary>
        /// Завершение заказа
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        //public async Task<ServiceResponseObject<BaseResponseObject>> CompleteOrder(Guid orderId)
        //{
        //    ServiceResponseObject<BaseResponseObject> response = new ServiceResponseObject<BaseResponseObject>();
        //    try
        //    {
        //        var task = await _boxContext.Tasks.Where(p => p.OrderId == orderId && p.IsCompleted == false).FirstOrDefaultAsync();
        //        task.DoneAt = DateTime.Now;
        //        task.IsCompleted = true;
        //        _boxContext.Update(task);
        //        _boxContext.SaveChanges();

        //        var driver = await _boxContext.Drivers.FindAsync(task.DriverId);
        //        driver.IsBusy = false;
        //        driver.BoxState = 1;
        //        _boxContext.Update(driver); _boxContext.SaveChanges();

        //        var orderHasBox = await _boxContext.OrderHasBoxes.Where(p => p.OrderId == orderId).FirstOrDefaultAsync();
        //        orderHasBox.IsBusy = false;
        //        _boxContext.Update(orderHasBox);
        //        _boxContext.SaveChanges();

        //        response.Message = "Заказ успешно выполнен.";
        //        response.Status = ResponseResult.OK;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = ResponseResult.Error;
        //        response.Message = "Что-то пошло не так... " + ex.Message;
        //        return response;
        //    }
        //}
    }
}
