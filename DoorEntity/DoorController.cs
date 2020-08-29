using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using itec_mobile_api_final.Data;
using itec_mobile_api_final.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace itec_mobile_api_final.DoorEntity
{
    
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UnauthorizedResult), 401)]
    public class DoorController: Controller
    {
        private readonly IRepository<DoorEntity> _doorEntity;
       
        public DoorController(ApplicationDbContext context)
        {
            _doorEntity = context.GetRepository<DoorEntity>();
        }

        [HttpPost("doorOpen")]
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
                await _doorEntity.UpdateAsync(doorEntity);
                return Ok();
            }

            return NotFound();
        }
        
        [HttpPost("doorClose")]
        [ProducesResponseType(typeof(IEnumerable<double>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> DoorClose()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId is null) return Unauthorized();
            var doorEntity=  _doorEntity.Queryable
                .LastOrDefault(t => t.UserId == userId);
            if (doorEntity != null)
            {
                doorEntity.IsOpen = false;
                await _doorEntity.UpdateAsync(doorEntity);
                return Ok();
            }

            return NotFound();
        }
        [HttpPost("/AddDoor")]
        public async Task<IActionResult> AddDoor()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId is null) return Unauthorized();
            var doorEntity = new DoorEntity();
            doorEntity.UserId = userId;
            doorEntity.IsOpen = false;
            var existDoorEntity =  _doorEntity.Queryable.FirstOrDefault(t => t.UserId == userId);
            if (existDoorEntity != null)
                return Forbid();


            await _doorEntity.AddAsync(doorEntity);
            return Ok();
        }
        
        
        /// <summary>
        /// Get Door Status
        /// </summary>
        [HttpGet("GetDoorStatus")]
        [ProducesResponseType(typeof(IEnumerable<DoorEntity>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetDoorStatus()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId is null) return Unauthorized();
          
            var doorEntity =  _doorEntity.Queryable.FirstOrDefault(t => t.UserId == userId);
            if (doorEntity == null )
                return NoContent();
            return Ok(doorEntity.IsOpen);
        }
    }
    
    
}
 
        

        
    
      
