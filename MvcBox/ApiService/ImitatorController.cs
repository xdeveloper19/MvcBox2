using Entities.Configuration;
using Entities.ViewModels;
using Entities.ViewModels.ImitatorViewModels;
using Entities.ViewModels.ImitatorViewModels.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MvcBox.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcBox.ApiService
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImitatorController : ControllerBase
    {
        private readonly IAuthService _userService;
        private readonly IDeviceService _devService;
        private readonly ILocationService _gpsService;
        private readonly ILogger<AuthController> _logger;
        private readonly AppSettings _appSettings;

        public ImitatorController(ILogger<AuthController> logger, IAuthService userService, IOptions<AppSettings> appSettings,
            IDeviceService deviceService, ILocationService location)
        {
            _logger = logger;
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _devService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
            _gpsService = location ?? throw new ArgumentNullException(nameof(location));
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LoginUser")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(string email, string userPassword)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim("CanEditContent","true")
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                var userData = await _userService.LoginUser(email, userPassword);
                if (userData.Result == DefaultEnums.Result.OK)
                {
                    await HttpContext.SignInAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme,
                  new ClaimsPrincipal(claimsIdentity),
                  authProperties);

                }
                return Ok(userData);
            }
            catch (Exception exp)
            {
                return BadRequest(new UserShortModel() { Error = exp, Result = DefaultEnums.Result.Error });
            }
        }

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegisterUser")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserModel model)
        {
            try
            {
                var userData = await _userService.Register(model);
                return Ok(userData);
            }
            catch (Exception exp)
            {
                return BadRequest(new UserShortModel() { Error = exp, Result = DefaultEnums.Result.Error });
            }
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("LogoutUser")]
        [Authorize]
        public async Task<IActionResult> LogoutUser()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                //var result = SignOut();
                return Ok(new { message = "Успешно выполнен выход" });
            }
            catch
            {
                return BadRequest(new { message = "Ошибка. попробуйте снова." });
            }
        }

        /// <summary>
        /// Регистрация нового мобильного устройства.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("RegisterDevice")]
        public async Task<IActionResult> RegisterDevice(DeviceModel model)
        {
            try
            {
                var userId = getUserId();
                if (userId == Guid.Empty)
                    return NotFound(new { message = "Такого пользователя нет в базе данных!" });

                var modelData = await _devService.RegisterDevice(model, userId);
                if (modelData.Result == DefaultEnums.Result.OK)
                    return Ok(modelData);
                else { throw new Exception(modelData.ErrorInfo); }
            }
            catch (Exception exp)
            {
                return BadRequest(new BaseModel() { Error = exp, Result = DefaultEnums.Result.Error });
            }
        }

        /// <summary>
        /// Добавление нового запроса.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatePhotoRequest")]
        public async Task<IActionResult> UpdatePhotoRequest(string IMEI)
        {
            try
            {
                await _devService.UpdatePhotoRequest(IMEI);
                return Ok(new { message = "Запрос успешно добавлен!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Поиск запроса на получение фото от сторонних пользователей.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("SearchPhotoRequest")]
        public async Task<IActionResult> SearchPhotoRequest(Guid id)
        {
            try
            {
                var response = await _devService.SearchPhotoRequest(id);
                if (response.Result == DefaultEnums.Result.OK)
                {
                    return Ok(response);
                }
                else
                {
                    throw new Exception(response.ErrorInfo);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new DeviceShortModel() { Error = ex, Result = DefaultEnums.Result.Error });
            }
        }

        /// <summary>
        /// Обновлнение GPS координат.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("SetGPS")]
        public async Task<IActionResult> SetGPS(PositionModel model)
        {
            try
            {
                await _gpsService.UpdateCoordinates(model);
                return Ok(new { message = "GPS координаты успешно обновлены!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "GPS сервисы недоступны. Попробуйте снова.\n Подробно: " + ex.Message });
            }
        }

        //public async Task<object> GetUsers(string search, string start, string length, DataTableModel.DtOrder[] order, DataTableModel.DtColumn[] columns)
        //{
        //    return await _userService.GetUsers(search, start, length, order, columns);
        //}
        //Получение ID пользователя
        private Guid getUserId()
        {
            try
            {
                var id = (Guid)HttpContext.Items["UserId"];
                return id;
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}
