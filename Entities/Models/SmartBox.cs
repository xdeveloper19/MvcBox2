using System;
using System.Collections.Generic;


namespace Entities.Models
{
    public class SmartBox
    {
        
        public enum ContainerState
        {
            //сложенный, то есть контейнер закрыт

            onBase = 1, //на складе
            onCar,//на автомобиле
            onShipper, //выгружен у грузоотправителя
            onConsignee//разгружен у грузополучателя
        }
        public SmartBox()
        {
            Alarms = new List<Alarm>();
            Media = new List<Media>();
            Variables = new List<Variable>();
            Events = new List<Event>();
            Sensors = new List<Sensor>();
            UserHasAccesses = new List<UserHasAccess>();
            Locations = new HashSet<Location>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        //public bool IsOpenedBox { get; set; }
        //public ContainerState BoxState { get; set; }
        //public bool IsOpenedDoor { get; set; }

        //[Range(0.0, 5000.0, ErrorMessage = "Значение веса должно быть от 0 до 5000 кг")]
        //public double Weight { get; set; }

        //[Range(0, 1023, ErrorMessage = "Значение освещенности должно быть от 0 до 1023")]
        //public int Light { get; set; }
        //public string Code { get; set; }

        //[Range(-40.0, 85.0, ErrorMessage = "Значение температуры должно быть от -40 до 85")]
        //public double Temperature { get; set; }

        //[Range(0.0, 100.0, ErrorMessage = "Значение влажности должно быть от 0 до 100")]
        //public double Wetness { get; set; }

        //[Range(0.0, 16.0, ErrorMessage = "Значение уровня заряда батареи должно быть от 0 до 16")]
        //public double BatteryPower { get; set; }
        //[MaxLength(45)]
        public string CloudKey { get; set; }
        public ICollection<Sensor> Sensors { get; set; }
        public ICollection<Media> Media { get; set; }
        public ICollection<Alarm> Alarms { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Variable> Variables { get; set; }
        public ICollection<UserHasAccess> UserHasAccesses { get; set; }
        public ICollection<Location> Locations { get; set; }
    }
}
