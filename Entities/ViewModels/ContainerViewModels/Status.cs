using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.ContainerViewModels
{
    public class Status: BaseResponseObject
    {
        public string id { get; set; }
        public Dictionary<string, string> Sensors { get; set; }
        public Status()
        {
            //this.Sensors = new Dictionary<string, string>()
            //{

            //    {"Температура","" },
            //    {"Влажность","" },
            //    {"Освещенность","" },
            //    {"Вес груза","" },
            //    {"Уровень заряда аккумулятора","" },
            //    {"Уровень сигнала","" },
            //    {"Состояние дверей","" },
            //    {"Состояние контейнера","" },
            //    {"Местоположение контейнера","" }
            //};
        }
    }
}
