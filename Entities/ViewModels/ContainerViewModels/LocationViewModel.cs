using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels.ContainerViewModels
{
    public class LocationViewModel
    {
        public Guid Id { get; set; }
        public double? Lon1 { get; set; }
        public double? Lat1 { get; set; }

        //[Range(-110.0, 0.0, ErrorMessage = "Значение сигнала должно быть от -110 до 0")]
        //public double Signal { get; set; }
        public DateTime Date { get; set; }
    }
}
