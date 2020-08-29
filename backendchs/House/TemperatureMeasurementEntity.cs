using System;
using itec_mobile_api_final.Entities;

namespace itec_mobile_api_final.Heater
{
    public class TemperatureMeasurementEntity : Entity
    {
        
        public int  HouseTemperature { get; set; }
        public int HeaterTemperature { get; set; }
        public int OutsideTemperature { get; set; }
        public String UserId { get; set; }
        public  DateTime MeasurementTime { get; set; }

        
    }
    
}