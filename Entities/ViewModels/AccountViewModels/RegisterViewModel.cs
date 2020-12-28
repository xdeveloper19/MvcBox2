using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите электронную почту")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните имя")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Имя должно быть минимум 3 символа и максимум 100")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Заполните фамилию")]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Выберите роль")]
        public string RoleName { get; set; }

    }
}
