using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.ViewModels;
using Entities.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Http;
using Entities.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Entities.Context;

namespace MvcBox.ApiService
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly SmartBoxContext _boxContext;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, SmartBoxContext boxContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
           _boxContext = boxContext;
        }

        //Получение сформированного ответа регистрации: "успешно" или описание ошибки...
        //...,а также получение фамилии,имени, логина
        //http://iot-tmc-cen.1gb.ru//api/auth/register
        [HttpPost]
        [Route("Register")]
        public async Task<ServiceResponseObject<AuthResponse>> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AuthMethods AuthData = new AuthMethods(_userManager, _signInManager, _roleManager, _boxContext);
                var Result = await AuthData.Register(model);
                return Result;
            }

         
           
            ServiceResponseObject<AuthResponse> response = new ServiceResponseObject<AuthResponse>();
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

        //Получение сформированного ответа авторизации: "Авторизация прошла успешно!" или ...
        //... "Неправильный логин и(или) пароль",а также получение фамилии,имени,логина
        //http://iot-tmc-cen.1gb.ru//api/auth/login
        [HttpPost]
        [Route("Login")]
        public async Task<ServiceResponseObject<AuthResponse>> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AuthMethods AuthData = new AuthMethods(_userManager, _signInManager, _roleManager, _boxContext);
                var Result = await AuthData.Login(model);
                return Result;
            }
            ServiceResponseObject<AuthResponse> response = new ServiceResponseObject<AuthResponse>();
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

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    //проверяем, принажлежит ли URL приложению
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            IdentityRole userRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == model.RoleName);

            if (userRole == null)
            {
                ModelState.AddModelError(string.Empty, "Укажите роль пользователя");
            }
            else if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName =
                    model.FirstName,
                    LastName = model.LastName
                };

                //Добавление пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Установка куки
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        // GET: api/Auth
        [HttpGet]
        [Route("Get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


    }
}
