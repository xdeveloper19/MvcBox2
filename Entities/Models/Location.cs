using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class Location
    {
        /*Выпадающий список объектов (первые 20) по  именованию, отправление айди выбранного объекта*/
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public DateTime CurrentDate { get; set; }
        public Guid BoxId { get; set; }
    }
}
