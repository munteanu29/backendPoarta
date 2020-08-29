using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using itec_mobile_api_final.Entities;
using Newtonsoft.Json;

namespace itec_mobile_api_final.DoorEntity
{
    public class DoorEntity : Entity
    {
       
       public bool IsOpen { get; set; }
        
        [ReadOnly(true)]
        public string UserId { get; set; }
    }
}