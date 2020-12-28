using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels.OrderViewModels
{
    public class PriceComputationViewModel
    {
        [Required(ErrorMessage ="Введите корректное значение адреса")]
        public double Length { get; set; }

        [Required(ErrorMessage = "Введите вес груза")]
        [Range(0.0, 5000.0, ErrorMessage = "Значение веса должно быть от 0 до 5000")]
        public double Weight { get; set; }
        public bool IsInsured { get; set; }
        [Required(ErrorMessage = "Введите ценность груза")]
        public double CargeValue { get; set; }
        [Required(ErrorMessage = "Выберите тип груза")]
        public string CargeType { get; set; }
        [Required(ErrorMessage = "Выберите класс опасности")]
        public string DangerClassType { get; set; }
    }
}
