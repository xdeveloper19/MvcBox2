using Entities.Context;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public class AuthMethods
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly SmartBoxContext _boxContext;


        public AuthMethods(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, SmartBoxContext boxContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _boxContext = boxContext;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResponseObject<AuthResponse>> Register(RegisterViewModel model)
        {
            ServiceResponseObject<AuthResponse> DataContent = new ServiceResponseObject<AuthResponse>();

            IdentityRole userRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == model.RoleName);
            if (userRole == null)
            {
                DataContent.Message = "Ошибка. Укажите роль пользователя.";
                DataContent.Status = ResponseResult.Error;
                return DataContent;
            }

            if (model == null)
            {
                DataContent.Message = "Не указаны данные для пользователя.";
                DataContent.Status = ResponseResult.Error;
                return DataContent;
            }

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

                DataContent.ResponseData = new AuthResponse()
                {
                    UserId = user.Id,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Role = model.RoleName
                };

                var rand = new Random();
                if (userRole.Name == "driver")
                {
                    Driver driver = new Driver
                    {
                        AccountId = user.Id,
                        Number = rand.Next(100000, 999999).ToString(),
                        IsBusy = false,
                        BoxState = 1
                    };
                    await _boxContext.Drivers.AddAsync(driver);
                    _boxContext.SaveChanges();

                    DataContent.ResponseData.DriverId = driver.Id;
                }

                DataContent.Message = "Успешно!";
                DataContent.Status = ResponseResult.OK;
                return DataContent;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    DataContent.Message += error.Description;
                }
                DataContent.Status = ResponseResult.Error;
                return DataContent;
            }
        }


        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResponseObject<AuthResponse>> Login(LoginViewModel model)
        {
            ServiceResponseObject<AuthResponse> DataContent = new ServiceResponseObject<AuthResponse>();
            User user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded && user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Count != 0)
                {
                    DataContent.ResponseData = new AuthResponse()
                    {
                        UserId = user.Id,
                        UserName = model.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = roles[0]
                    };

                    //if (roles[0] == "driver")
                    //{
                    //    var driver = await _boxContext.Drivers.Where(p => p.AccountId == user.Id).FirstOrDefaultAsync();
                    //    DataContent.ResponseData.DriverId = driver.Id;
                    //}

                    DataContent.Message = "Авторизация прошла успешно!";
                    DataContent.Status = ResponseResult.OK;
                    return DataContent;
                }
                DataContent.Message = "У пользователя не определена роль. Зарегистрируйтесь и попробуйте снова.";
                DataContent.Status = ResponseResult.Error;
                return DataContent;
            }
            else
            {
                DataContent.Message = "Неправильный логин и(или) пароль";
                DataContent.Status = ResponseResult.Error;
                return DataContent;
            }
        }
    }
}
