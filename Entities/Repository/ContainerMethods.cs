using Entities.Context;
using Entities.Models;
using Entities.Repository.DefaultData;
using Entities.ViewModels;
using Entities.ViewModels.ContainerViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public class ContainerMethods
    {
        private readonly SmartBoxContext _boxContext;
        private readonly ImitatorContext _imContext;

        public ContainerMethods(SmartBoxContext boxContext)
        {
            _boxContext = boxContext;
        }

        /// <summary>
        /// Добавление контейнера в бд
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ServiceResponseObject<ContainerResponse>> Create(string name)
        {
            ServiceResponseObject<ContainerResponse> DataContent = new ServiceResponseObject<ContainerResponse>();

            var box = _boxContext.SmartBoxes.Where(s => s.Name == name).FirstOrDefault();
            if (box != null)
            {
                box.CloudKey = "1";
                _boxContext.Update(box);
                await _boxContext.SaveChangesAsync();

                DataContent.Status = ResponseResult.OK;
                DataContent.Message = "Объект найден!";
                DataContent.ResponseData = new ContainerResponse
                {
                    SmartBoxId = box.Id,
                    Name = box.Name
                };
                return DataContent;
            }

            box = new SmartBox
            {
                Name = name,
                CloudKey = "1"
            };

            var result = await _boxContext.SmartBoxes.AddAsync(box);
            _boxContext.SaveChanges();

            SeedData.InitializeSensors(_boxContext, box.Id);

            DataContent.Status = ResponseResult.OK;
            DataContent.Message = "Объект успешно добавлен!";
            DataContent.ResponseData = new ContainerResponse
            {
                SmartBoxId = box.Id,
                Name = box.Name
            };
            return DataContent;
        }
        
        /// <summary>
        /// Вызов тревоги
        /// </summary>
        /// <param name="IMEI"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<AlarmResponse> RaiseAlarm(string IMEI, string option)
        {
            int alarmNumber = int.Parse(option);
            var boxId = getBoxId(IMEI);
            var alarm = await _boxContext.Alarms.Where(s => s.Number == alarmNumber && s.BoxId == boxId && s.Active == true).FirstOrDefaultAsync();

            if (alarm != null)
            {
                var response = new AlarmResponse
                {
                    Message = "Тревога уже вызвана."
                };

                return response;
            }

            _boxContext.Alarms.Add(new Alarm
            {
                Number = alarmNumber,
                Active = true,
                Message = AlarmDefaultData.Messages[alarmNumber],
                AlarmTypeId = AlarmDefaultData.TypeId[alarmNumber],
                BoxId = boxId,
                Acknowledge = true,
                AcknowledgedAt = DateTime.Now
            });

            await _boxContext.SaveChangesAsync();
            var data = new AlarmResponse
            {
                Message = "Новая тревога успешно добавлена."
            };

            return data;
        }

        /// <summary>
        /// Отмена тревоги
        /// </summary>
        /// <param name="IMEI"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<AlarmResponse> ReleaseAlarm(string IMEI, string option)
        {
            int alarmNumber = int.Parse(option);
            var boxId = getBoxId(IMEI);
            var alarm = await _boxContext.Alarms.Where(s => s.Number == alarmNumber && s.BoxId == boxId && s.Active == true).FirstOrDefaultAsync();

            if (alarm == null)
            {
                var response = new AlarmResponse
                {
                    Message = "Тревога не была вызвана."
                };

                return response;
            }

            alarm.Active = false;
            alarm.ReleasedAt = DateTime.Now;
            _boxContext.Update(alarm);
            await _boxContext.SaveChangesAsync();

            var result = new AlarmResponse
            {
                Message = "Тревога успешно отменена."
            };

            return result;
        }

        /// <summary>
        /// Логгирование объекта по GPS
        /// </summary>
        /// <param name="model.Id"></param>
        /// <param name="model.Lon1"></param>
        /// <param name="model.Lat1"></param>
        /// <param name="model.Date"></param>
        /// <returns></returns>
        public async Task<ServiceResponseObject<BaseResponseObject>> SetContainerLocation(LocationViewModel model)
        {
            SmartBox box = await _boxContext.SmartBoxes.FirstOrDefaultAsync(s => s.Id == model.Id);
            ServiceResponseObject<BaseResponseObject> DataContent = new ServiceResponseObject<BaseResponseObject>();


            if (box != null)
            {
                Location location = new Location
                {
                    BoxId = box.Id,
                    Latitude = model.Lat1,
                    Longitude = model.Lon1,
                    CurrentDate = model.Date
                };

                _boxContext.Locations.Add(location);
                await _boxContext.SaveChangesAsync();
                DataContent.Message = "Успешно!";
                DataContent.Status = ResponseResult.OK;
                return DataContent;
            }

            DataContent.Message = "Контейнер не найден!";
            DataContent.Status = ResponseResult.Error;
            return DataContent;
        }

        /// <summary>
        /// Получение данных контейнера
        /// </summary>
        /// <param name="IMEI">IMEI контейнера</param>
        /// <returns></returns>
        public async Task<ServiceResponseObject<ListResponse<BoxDataResponse>>> GetBox(string IMEI)
        {
            ServiceResponseObject<ListResponse<BoxDataResponse>> ContentData = new ServiceResponseObject<ListResponse<BoxDataResponse>>();
            var box = await _boxContext.SmartBoxes.Where(s => s.Name == IMEI).FirstOrDefaultAsync();

            if (box != null)
            {

                _boxContext.Entry(box)
            .Collection(c => c.Sensors)
            .Load();

                ContentData.ResponseData = new ListResponse<BoxDataResponse>();

                foreach (var sensor in box.Sensors)
                {
                    _boxContext.Entry(sensor)
                        .Collection(c => c.SensorDatas)
                        .Load();

                    if (sensor.SensorDatas != null && sensor.SensorDatas.Count != 0)
                    {
                        var data = sensor.SensorDatas.OrderBy(p => p.CreatedAt).Select(s => new BoxDataResponse
                        {
                            CreatedAt = s.CreatedAt,
                            SensorName = s.SensorName,
                            Value = s.Value
                        }).LastOrDefault();

                        ContentData.ResponseData.Objects.Add(data);
                    }
                }

                ContentData.Message = "Данные успешно получены.";
                ContentData.Status = ResponseResult.OK;
                return ContentData;
            }
            else
            {
                ContentData.Message = "Ошибка, нет данных.";
                ContentData.Status = ResponseResult.Error;
                return ContentData;
            }
        }

        /// <summary>
        /// Редактирование данных объекта
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResponseObject<BaseResponseObject>> EditSensors(EditBoxViewModel model)
        {
            ServiceResponseObject<BaseResponseObject> ContentData = new ServiceResponseObject<BaseResponseObject>();
            if (model == null)
            {
                ContentData.Message = "Не указаны данные для контейнера.";
                ContentData.Status = ResponseResult.Error;
                return ContentData;
            }
            var box = await _boxContext.SmartBoxes.FirstOrDefaultAsync(s => s.Name == model.Id);
            //var team = await _teamContext.Teams.FirstOrDefaultAsync(f => f.Id == user.TeamId);

            if (box == null)
            {
                ContentData.Message = "Контейнер не найден.";
                ContentData.Status = ResponseResult.Error;
                return ContentData;
            }

            _boxContext.Entry(box)
                        .Collection(c => c.Sensors)
                        .Load();

            var date = DateTime.Now;

            foreach (var sensor in box.Sensors)
            {
                _boxContext.Entry(sensor)
                       .Collection(c => c.SensorDatas)
                       .Load();

                _boxContext.SensorDatas.Add(new SensorData
                {
                    CreatedAt = date,
                    SensorId = sensor.Id,
                    SensorName = sensor.Name,
                    Value = model.Sensors.Where(f => f.Key == sensor.Name).Select(s => s.Value).FirstOrDefault()
                }) ;

                await _boxContext.SaveChangesAsync();
            }

            _boxContext.Update(box);
            await _boxContext.SaveChangesAsync();
            ContentData.Message = "Успешно.";
            ContentData.Status = ResponseResult.OK;
            return ContentData;
        }

        private Guid getBoxId(string IMEI)
        {
            var box = _boxContext.SmartBoxes.Where(s => s.Name == IMEI).FirstOrDefault();
            return box.Id;
        }

        #region Obsolete

        /// <summary>
        /// Поиск записи в БД о наличии запроса на фото
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ServiceResponseObject<BaseResponseObject>> SearchCommandPhoto(string name)
        {
            ServiceResponseObject<BaseResponseObject> DataContent = new ServiceResponseObject<BaseResponseObject>();

            var box = _boxContext.SmartBoxes.Where(s => s.Name == name && s.CloudKey == "1").FirstOrDefault();
            if (box != null)
            {
                box.CloudKey = "0";
                _boxContext.Update(box);
                await _boxContext.SaveChangesAsync();

                DataContent.Status = ResponseResult.OK;
                DataContent.Message = "Запрос от клиента на получение фото.";
                return DataContent;
            }


            DataContent.Status = ResponseResult.Error;
            DataContent.Message = "Запросов нет.";
            return DataContent;
        }

        /// <summary>
        /// Получение показаний имитатора
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ServiceResponseObject<BoxResponse>> GetImitatorSensors(string id)
        {
            ServiceResponseObject<BoxResponse> DataContent = new ServiceResponseObject<BoxResponse>();

            try
            {
                var myHttpClient = new HttpClient();

                var uri2 = new Uri("http://smartboxcity.ru:8003/imitator/status?id=" + id);
                HttpResponseMessage response = await myHttpClient.GetAsync(uri2.ToString());
                //var myHttpClient = new HttpClient();
                //var id1 = CrossSettings.Current.GetValueOrDefault("id", "");
                //var uri = new Uri("http://iot.tmc-centert.ru/api/container/getbox?id=" + id1);
                // HttpResponseMessage response = await myHttpClient.GetAsync(uri.ToString());

                string s_result;
                using (HttpContent responseContent = response.Content)
                {
                    s_result = await responseContent.ReadAsStringAsync();
                }

                if (response.IsSuccessStatusCode)
                {
                    DataContent.ResponseData = JsonConvert.DeserializeObject<BoxResponse>(s_result);
                    DataContent.Status = ResponseResult.OK;
                    DataContent.Message = "Успешно!";
                    return DataContent;
                }
                ErrorResponse error = new ErrorResponse();
                error = JsonConvert.DeserializeObject<ErrorResponse>(s_result);

                DataContent.Message = error.Errors[0];
                DataContent.Status = ResponseResult.Error;
                return DataContent;
            }
            catch (Exception ex)
            {
                DataContent.Message = ex.Message;
                DataContent.Status = ResponseResult.Error;
                return DataContent;
            }
        }

        /// <summary>
        /// Тестирование контроллеров.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="temp"></param>
        /// <param name="light"></param>
        /// <param name="humidity"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<ServiceResponseObject<BaseResponseObject>> TestSensors(string id, string temp, string light, string humidity)
        {
            ServiceResponseObject<BaseResponseObject> ContentData = new ServiceResponseObject<BaseResponseObject>();

            //var model = _boxContext.SmartBoxes.Where(s => s.Name == id).FirstOrDefault();
            //if (model == null)
            //{
            //    ContentData.Message = "Неправильный MD5 IMEI.";
            //    ContentData.Status = ResponseResult.Error;
            //    return ContentData;
            //}

            try
            {
                var myHttpClient = new HttpClient();
                var uri2 = new Uri("http://smartboxcity.ru:8003/imitator/status?id=" + id);
                HttpResponseMessage response = await myHttpClient.GetAsync(uri2.ToString());

                string s_result;
                using (HttpContent responseContent = response.Content)
                {
                    s_result = await responseContent.ReadAsStringAsync();
                }

                if (response.IsSuccessStatusCode)
                {
                    ViewModels.ContainerViewModels.BoxResponse o_data = new ViewModels.ContainerViewModels.BoxResponse();
                    o_data.status.Sensors = new Dictionary<string, string>()
                    {

                        {"Температура","" },
                        {"Влажность","" },
                        {"Освещенность","" },
                        {"Вес груза","" },
                        {"Уровень заряда аккумулятора","" },
                        {"Уровень сигнала","" },
                        {"Состояние дверей","" },
                        {"Состояние контейнера","" },
                        {"Местоположение контейнера","" }
                    };
                    o_data = JsonConvert.DeserializeObject<ViewModels.ContainerViewModels.BoxResponse>(s_result);

                    o_data.status.Sensors["Температура"] = temp;
                    o_data.status.Sensors["Влажность"] = humidity;
                    o_data.status.Sensors["Освещенность"] = light;

                    ViewModels.ContainerViewModels.Status ForAnotherServer = new ViewModels.ContainerViewModels.Status
                    {
                        id = id,

                        Sensors = new Dictionary<string, string>
                        {
                            ["Вес груза"] = o_data.status.Sensors["Вес груза"],
                            ["Температура"] = o_data.status.Sensors["Температура"],
                            ["Влажность"] = o_data.status.Sensors["Влажность"],
                            ["Освещенность"] = o_data.status.Sensors["Освещенность"],
                            ["Уровень заряда аккумулятора"] = o_data.status.Sensors["Уровень заряда аккумулятора"],
                            ["Уровень сигнала"] = o_data.status.Sensors["Уровень сигнала"],
                            ["Состояние дверей"] = o_data.status.Sensors["Состояние дверей"],
                            ["Состояние контейнера"] = o_data.status.Sensors["Состояние контейнера"],
                            ["Местоположение контейнера"] = o_data.status.Sensors["Местоположение контейнера"]
                        },
                    };

                    var uri3 = new Uri("http://smartboxcity.ru:8003/imitator/sensors?" + "id=" + o_data.status.id + "&sensors[Вес груза]=" + o_data.status.Sensors["Вес груза"]
                    + "&sensors[Температура]=" + o_data.status.Sensors["Температура"] + "&sensors[Влажность]=" + o_data.status.Sensors["Влажность"] + "&sensors[Освещенность]=" + o_data.status.Sensors["Освещенность"]
                    + "&sensors[Уровень заряда аккумулятора]=" + o_data.status.Sensors["Уровень заряда аккумулятора"] + "&sensors[Уровень сигнала]=" + o_data.status.Sensors["Уровень сигнала"] + "&sensors[Состояние дверей]=" + o_data.status.Sensors["Состояние дверей"]
                    + "&sensors[Состояние контейнера]=" + o_data.status.Sensors["Состояние контейнера"] + "&sensors[Местоположение контейнера]=" + o_data.status.Sensors["Местоположение контейнера"]);


                    // var uri3 = new Uri("http://smartboxcity.ru:8003/imitator/sensors");
                    HttpResponseMessage responseFromAnotherServer = await myHttpClient.PostAsync(uri3.ToString(), new StringContent(JsonConvert.SerializeObject(ForAnotherServer), Encoding.UTF8, "application/json"));

                    string s_result_from_server;
                    using (HttpContent responseContent = responseFromAnotherServer.Content)
                    {
                        s_result_from_server = await responseContent.ReadAsStringAsync();
                    }



                    if (responseFromAnotherServer.StatusCode == HttpStatusCode.OK)
                    {
                        ContentData = JsonConvert.DeserializeObject<ServiceResponseObject<BaseResponseObject>>(s_result_from_server);
                        return ContentData;
                    }
                    ErrorResponse error2 = new ErrorResponse();
                    error2 = JsonConvert.DeserializeObject<ErrorResponse>(s_result_from_server);

                    ContentData.Message = error2.Errors[0];
                    ContentData.Status = ResponseResult.Error;
                    return ContentData;
                }

                ErrorResponse error = new ErrorResponse();
                error = JsonConvert.DeserializeObject<ErrorResponse>(s_result);

                ContentData.Message = error.Errors[0];
                ContentData.Status = ResponseResult.Error;
                return ContentData;

            }
            catch (Exception ex)
            {
                ContentData.Message = ex.Message;
                ContentData.Status = ResponseResult.Error;
                return ContentData;
            }
        }

        /// <summary>
        /// Получение рандомного контейнера
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponseObject<GetBoxIdResponse>> GetRandomBox()
        {
            ServiceResponseObject<GetBoxIdResponse> DataContent = new ServiceResponseObject<GetBoxIdResponse>();
            var Item = await _boxContext.SmartBoxes.OrderBy(o => Guid.NewGuid()).FirstOrDefaultAsync();

            if (Item == null)
            {
                DataContent.Status = ResponseResult.Error;
                DataContent.Message = "Объекты отсутствуют.";
                return DataContent;
            }

            DataContent.Status = ResponseResult.OK;
            DataContent.Message = "Успешно.";
            DataContent.ResponseData = new GetBoxIdResponse
            {
                BoxId = Item.Id.ToString(),
                Name = Item.Name
            };
            return DataContent;
        }

        /// <summary>
        /// Редактирование состояния контейнера
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public async Task<ServiceResponseObject<BaseResponseObject>> EditConditions(EditConditionsViewModel model)
        //{
        //    ServiceResponseObject<BaseResponseObject> ContentData = new ServiceResponseObject<BaseResponseObject>();
        //    if (model == null)
        //    {
        //        ContentData.Message = "Не указаны данные для контейнера.";
        //        ContentData.Status = ResponseResult.Error;
        //        return ContentData;
        //    }
        //    var box = await _boxContext.SmartBoxes.FirstOrDefaultAsync(s => s.Id == model.BoxId);
        //    //var team = await _teamContext.Teams.FirstOrDefaultAsync(f => f.Id == user.TeamId);

        //    if (box == null)
        //    {
        //        ContentData.Message = "Контейнер не найден.";
        //        ContentData.Status = ResponseResult.Error;
        //        return ContentData;
        //    }

        //    box.Name = model.Name;
        //    box.BoxState = model.BoxState;
        //    box.BatteryPower = model.BatteryPower;
        //    box.Code = model.Code;
        //    box.IsOpenedBox = model.IsOpenedBox;
        //    box.IsOpenedDoor = model.IsOpenedDoor;
        //    box.Light = model.Light;
        //    box.Temperature = model.Temperature;
        //    box.Weight = model.Weight;
        //    box.Wetness = model.Wetness;

        //    _boxContext.Update(box);
        //    await _boxContext.SaveChangesAsync();
        //    ContentData.Message = "Успешно.";
        //    ContentData.Status = ResponseResult.OK;
        //    return ContentData;
        //}

        /// <summary>
        /// Получение первых 20 контейнеров
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponseObject<ListResponse<ContainerResponse>>> GetBoxesByName(string name)
        {
            var boxes = await _boxContext.SmartBoxes.Where(b => b.Name.ToLower().StartsWith(name.ToLower())).OrderBy(b => b.Name).Take(20).Select(b => new ContainerResponse
            {
                Name = b.Name,
                SmartBoxId = b.Id
            }).ToListAsync();

            ServiceResponseObject<ListResponse<ContainerResponse>> ContentData = new ServiceResponseObject<ListResponse<ContainerResponse>>();
            if (boxes != null && boxes.Count != 0)
            {
                ContentData.ResponseData = new ListResponse<ContainerResponse>
                {
                    Objects = boxes
                };
                ContentData.Message = "Успешно!";
                ContentData.Status = ResponseResult.OK;
                return ContentData;
            }
            ContentData.Message = "Ничего не найдено.";
            ContentData.Status = ResponseResult.Error;
            return ContentData;
        }

        /// <summary>
        /// Получение последних координат каждого объекта
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponseObject<ListResponse<BoxLocation>>> GetBoxesLocation()
        {
            ServiceResponseObject<ListResponse<BoxLocation>> ContentData = new ServiceResponseObject<ListResponse<BoxLocation>>();
            var boxes = await _boxContext.SmartBoxes.ToListAsync();

            if (boxes == null || boxes.Count == 0)
            {
                ContentData.Message = "Контейнеры не добавлены еще в базу данных.";
                ContentData.Status = ResponseResult.Error;
                return ContentData;
            }

            //Загрузка связанных данных
            ContentData.ResponseData = new ListResponse<BoxLocation>();
            foreach (var box in boxes)
            {
                _boxContext.Entry(box)
            .Collection(c => c.Locations)
            .Load();

                //var sendors = GetBox(box.Id);

                if (box.Locations == null || box.Locations.Count == 0)
                    continue;

                // Получить последние координаты с объекта
                var lastItem = box.Locations.OrderBy(p => p.CurrentDate).Select(f => new BoxLocation
                {
                    SmartBoxId = f.BoxId,
                    Latitude = f.Latitude,
                    Longitude = f.Longitude,
                    BatteryPower = 13,
                    IsOpenedDoor = true,
                    BoxState = SmartBox.ContainerState.onConsignee,
                    Code = "1234",
                    IsOpenedBox = false,
                    Light = 123,
                    Temperature = 23,
                    Weight = 1234,
                    Wetness = 23
                }
                ).Last();

                if (lastItem != null)
                    ContentData.ResponseData.Objects.Add(lastItem);
            }

            if (ContentData.ResponseData.Objects.Count == 0)
            {
                ContentData.Message = "Геолокация не найдена.";
                ContentData.Status = ResponseResult.Error;
                return ContentData;
            }

            ContentData.Message = "Успешно!";
            ContentData.Status = ResponseResult.OK;
            return ContentData;
        }

        #endregion

    }
}
