using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.ContainerViewModels
{
    public class SensorResponse: BaseResponseObject
    {
        public SensorResponse()
        {
            this.sensors_status = new SensorDataResponse();
        }
        public string id { get; set; }
        public string driver_id { get; set; }
        public string cloud_key { get; set; }
        public string owner_id { get; set; }
        public string vpn_ip { get; set; }
        public string busy { get; set; }
        public string order_id { get; set; }
        public SensorDataResponse sensors_status { get; set; }
        public int event_count { get; set; }
        public object alarms_status { get; set; }
    }

    public class SensorDataResponse 
    {
        public string battery { get; set; }
        public string weight { get; set; }
        public string temperature { get; set; }
        public string humidity { get; set; }
        public string illumination { get; set; }
        public string gate { get; set; }
        public string Lock { get; set; }
        public string fold { get; set; }
    }
}
