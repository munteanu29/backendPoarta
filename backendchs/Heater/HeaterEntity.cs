using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using itec_mobile_api_final.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace itec_mobile_api_final.Heater
{
    public class HeaterEntity : Entity
    {
        public bool IsOn { get; set; }
        public int  Temperature { get; set; }
        public int SetTemperature { get; set; }
        
        [ReadOnly(true)]
        public string UserId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        






    }
    
}