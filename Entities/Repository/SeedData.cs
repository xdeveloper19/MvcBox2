using Entities.Context;
using Entities.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Repository
{
    public class SeedData
    {
        //public static void InitializeOrderStages(IServiceProvider serviceProvider)
        //{
        //    using (var context = new SmartBoxContext(serviceProvider.GetRequiredService<Microsoft.EntityFrameworkCore.DbContextOptions<SmartBoxContext>>()))
        //    {
        //        //Look for any movies
        //        if (context.OrderStages.Any())
        //        {
        //            return; //DB has been seeded
        //        }

        //        context.OrderStages.AddRange(
        //                new OrderStage
        //                {
        //                    Name = "Заявка"
        //                },

        //                new OrderStage
        //                {
        //                    Name = "Ожидает оплату"
        //                },

        //                new OrderStage
        //                {
        //                    Name = "Оплачен"
        //                },

        //                new OrderStage
        //                {
        //                    Name = "Доставка в пункт загрузки"
        //                },

        //                new OrderStage
        //                {
        //                    Name = "Ожидание загрузки"
        //                },

        //                new OrderStage
        //                {
        //                    Name = "Выгрузка"
        //                },

        //                new OrderStage
        //                {
        //                    Name = "Завершение"
        //                },

        //                new OrderStage
        //                {
        //                    Name = "Выполнен"
        //                }
        //                );
        //        context.SaveChanges();
        //    }
        //}

        public static void InitializeSensorType(IServiceProvider serviceProvider)
        {
            using (var context = new SmartBoxContext(serviceProvider.GetRequiredService<Microsoft.EntityFrameworkCore.DbContextOptions<SmartBoxContext>>()))
            {
                //Look for any movies
                if (context.SensorTypes.Any())
                {
                    return; //DB has been seeded
                }

                context.SensorTypes.AddRange(
                        new SensorType
                        {
                            Name = "Дискретный"
                        },

                        new SensorType
                        {
                            Name = "Аналоговый"
                        }

                       );
                context.SaveChanges();
            }
        }

        public static void InitializeSensors(SmartBoxContext context, Guid boxId)
        {
            var box = context.SmartBoxes.Find(boxId);

            context.Entry(box)
            .Collection(c => c.Sensors)
            .Load();

            //Look for any entries
            if (box.Sensors.Any())
             {
                 return; //DB has been seeded
             }

            var sensorTypeId = context.SensorTypes.Where(f => f.Name == "Дискретный").Select(s => s.Id).FirstOrDefault();
            context.Sensors.AddRange(
                    new Sensor
                    {
                        Name = "Вес груза",
                        SensorTypeId = sensorTypeId,
                        BoxId = boxId
                    },


                    new Sensor
                    {
                        Name = "Температура",
                        SensorTypeId = sensorTypeId,
                        BoxId = boxId
                    },

                    new Sensor
                    {
                        Name = "Влажность",
                        SensorTypeId = sensorTypeId,
                        BoxId = boxId
                    },

                    new Sensor
                    {
                        Name = "Освещенность",
                        SensorTypeId = sensorTypeId,
                        BoxId = boxId
                    },

                    new Sensor
                    {
                        Name = "Уровень заряда аккумулятора",
                        SensorTypeId = sensorTypeId,
                        BoxId = boxId
                    },

                    new Sensor
                    {
                        Name = "Уровень сигнала",
                        SensorTypeId = sensorTypeId,
                        BoxId = boxId
                    },

                    new Sensor
                    {
                        Name = "Состояние дверей",
                        SensorTypeId = sensorTypeId,
                        BoxId = boxId
                    },

                    new Sensor
                    {
                        Name = "Состояние контейнера",
                        SensorTypeId = sensorTypeId,
                        BoxId = boxId
                    },

                    new Sensor
                    {
                        Name = "Местоположение контейнера",
                        SensorTypeId = sensorTypeId,
                        BoxId = boxId
                    }
                   ) ;
            context.SaveChanges();

            foreach (var sensor in box.Sensors)
            {
                context.SensorDatas.AddRange(
                    new SensorData
                    {
                        CreatedAt = DateTime.Now,
                        SensorId = sensor.Id,
                        SensorName = sensor.Name,
                        Value = "0"
                    }
                   ) ;
                context.SaveChanges();
            }
        }

        public static void InitializeTestContainer(SmartBoxContext context, List<string> keys)
        {
            foreach (var boxId in keys)
            {
                var box = context.SmartBoxes.Where(s => s.Name == boxId).FirstOrDefault();

                if (box == null)
                {
                    SmartBox box1 = new SmartBox
                    {
                        Name = boxId
                    };

                    context.SmartBoxes.AddRange(box1);
                    context.SaveChanges();
                }
            }
        }
    }
}
