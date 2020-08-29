using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using itec_mobile_api_final.Base;
using itec_mobile_api_final.Data;
using itec_mobile_api_final.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using itec_mobile_api_final.HeaterSchedule;
using Microsoft.AspNetCore.Http;
using Microsoft.ML;
//using Microsoft.ML;



namespace itec_mobile_api_final.Heater
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UnauthorizedResult), 401)]
    public class HeaterController : Controller
    {
        private readonly IRepository<HeaterEntity> _heaterRepository;
        private readonly IRepository<HeaterScheduleEntity> _heaterScheduleRepository;
        
        public HeaterController(ApplicationDbContext context)
        {
            _heaterRepository = context.GetRepository<HeaterEntity>();
            _heaterScheduleRepository = context.GetRepository<HeaterScheduleEntity>();

        }
        
        /// <summary>
        /// Get Temperature
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HeaterEntity>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetTemperature()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId is null) return Unauthorized();
            
          
            var temperature =  _heaterRepository.Queryable.FirstOrDefault(t => t.UserId == userId);
            if (temperature == null )
                return NoContent();
            
            var heaterScheduleEntity = _heaterScheduleRepository.Queryable.Where(t => t.UserId == userId)
                .OrderBy(t => t.HeaterFinishedTime).LastOrDefault();
            if (heaterScheduleEntity.HeaterStartTime > DateTime.Now &&
                temperature.Temperature <= heaterScheduleEntity.FinalHouseTemperature)
                temperature.IsOn = true;

            return Ok(temperature);
        }
        

        /// <summary>
        ///Set Temperature
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(HeaterEntity), 200)]
        public async Task<IActionResult> SetTemperature(int temperature)
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId is null) return Unauthorized();

            var existing = _heaterRepository.Queryable.FirstOrDefault(entity =>entity.UserId==userId);
            if (existing == null)
                return NoContent();

            existing.SetTemperature = temperature;
            await _heaterRepository.UpdateAsync(existing);
                if (existing.Temperature < existing.SetTemperature)
                {
                    existing.IsOn = true;
                }
                else if (existing.Temperature >= existing.SetTemperature)
                {
                    existing.IsOn = false;
                }
                return Ok(existing);
            

            
        }
        /// <summary>
        ///Increase Temperature
        /// </summary>
        [HttpPost("/IncreaseTemperature")]
        [ProducesResponseType(typeof(HeaterEntity), 200)]
        public async Task<IActionResult> IncreaseTemperature()
        {   PortChat portChat = new PortChat();
            int dif;
            var userId = HttpContext.GetCurrentUserId();
            if (userId is null) return Unauthorized();

            var existing = await _heaterRepository.GetAsync("1");
            
            existing.Temperature ++;
            await _heaterRepository.UpdateAsync(existing);
            portChat.Write((existing.SetTemperature-existing.Temperature+100).ToString());

            
            return Ok();
        }
        
        /// <summary>
        ///Decrease Temperature
        /// </summary>
        [HttpPost("DecreaseTemperature")]
        [ProducesResponseType(typeof(HeaterEntity), 200)]
        public async Task<IActionResult> DecreaseTemperature()
        {   PortChat portChat = new PortChat();
            var userId = HttpContext.GetCurrentUserId();
            if (userId is null) return Unauthorized();

            var existing = await _heaterRepository.GetAsync("1");
            
            existing.Temperature --;
            portChat.Write((existing.SetTemperature-existing.Temperature+100).ToString());
            await _heaterRepository.UpdateAsync(existing);
            return Ok();
        }
        
        [HttpPost("/Add")]
        public async Task<IActionResult> AddHeater()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId is null) return Unauthorized();
            var heaterEntity = new HeaterEntity();
            heaterEntity.UserId = userId;
            heaterEntity.Temperature = 0;
            heaterEntity.IsOn = false;
            heaterEntity.SetTemperature = 0;

            await _heaterRepository.AddAsync(heaterEntity);
            return Ok();
        }


        
    }
}