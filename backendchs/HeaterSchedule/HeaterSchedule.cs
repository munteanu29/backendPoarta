using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using itec_mobile_api_final.Entities;
using Newtonsoft.Json;

namespace itec_mobile_api_final.HeaterSchedule
{
    public class HeaterScheduleEntity : Entity
    {
       
        public DateTime HeaterStartTime { get; set; }
        public DateTime HeaterFinishedTime { get; set; }
        public float InitialHouseTemperature { get; set; }
        public float InitialOutsideTemperature { get; set; }
        public float FinalOutsideTemperature { get; set; }
        public float FinalHouseTemperature { get; set; }
        public float HeaterAverageTemperature { get; set; }
        public float OutsideAverageTemperature { get; set; }
        public float HeatingTime { get; set; }
        
        [ReadOnly(true)]
        public string UserId { get; set; }
    }
}