using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using itec_mobile_api_final.Data;
using itec_mobile_api_final.Extensions;
using itec_mobile_api_final.Heater;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Newtonsoft.Json;

namespace itec_mobile_api_final.HeaterSchedule
{
    
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UnauthorizedResult), 401)]
    public class HeaterScheduleController: Controller
    {
        private readonly IRepository<DoorEntity> _doorEntity;
       
        public HeaterScheduleController(ApplicationDbContext context)
        {
            _doorEntity = context.GetRepository<DoorEntity>();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<double>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> DoorOpen()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId is null) return Unauthorized();
            var doorEntity=  _doorEntity.Queryable
                .LastOrDefault(t => t.UserId == userId);
            if (doorEntity != null)
            {
                doorEntity.IsOpen = true;
                return Ok();
            }

            return NotFound();
        }
//        public async Task<IActionResult> GetSchedule()
//        {
//            var userId = HttpContext.GetCurrentUserId();
//            if (userId is null) return Unauthorized();
//
//            var heaterScheduleEntity = _heaterScheduleRepository.Queryable.Where(t => t.UserId == userId)
//                .OrderBy(t => t.HeaterFinishedTime).LastOrDefault();
//            if (heaterScheduleEntity == null)
//                return NoContent();
//            var heaterFinishedTime = (heaterScheduleEntity.HeaterStartTime.Ticks-621355968000000000)/10000000- 3600*3;
//            
//           
//            Console.WriteLine("!!!!!!!!!!!!!!!!!!!"+heaterFinishedTime);
//            return Ok(heaterFinishedTime);
//        }
    }
}
 
        

        
    
      
