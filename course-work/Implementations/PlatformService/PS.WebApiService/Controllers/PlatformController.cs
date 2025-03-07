using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PS.ApplicationServices.Interfaces;
using PS.ApplicationServices.Messaging;
using PS.ApplicationServices.Messaging.Requests.Platforms;
using PS.ApplicationServices.Messaging.Responses.OperatingSystem;
using PS.ApplicationServices.Messaging.Responses.Platforms;
using PS.Infrastructure.Messaging.Requests.Platforms;
using PS.Infrastructure.Messaging.Responses.Platforms;

namespace PS.WebApiService.Controllers
{
    /// <summary>
    /// Platform controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    [Produces("application/json")]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformManagementService _platformService;

        /// <summary>
        /// Platform controller object
        /// </summary>
        /// <param name="platformService"></param>
        public PlatformController(IPlatformManagementService platformService)
        {
            _platformService = platformService;
        }

        /// <summary>
        /// Get platforms
        /// </summary>
        /// <returns>Return list of platfoms.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetPlatformResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status404NotFound)]
        [HttpGet("{id}", Name = "GetPlatformById")]
        public async Task<IActionResult> GetPlatformById(int id)
        {
            if(id == 0)
            {
                return Ok(await _platformService.GetPlatformsAsync(new( -1)));
            }
            return Ok(await _platformService.GetPlatformByIdAsync(new(id)));
        } 


        /// <summary>
        /// Create new platform.
        /// </summary>
        /// <param name="platform">Platform model object.</param>
        /// <returns>Return create empty response.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreatePlatformResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]        
        public async Task<IActionResult> CreatePlatform([FromBody] CreatePlatformRequest platform)
        {
            var response =  await _platformService.CreatePlatformAsync(new(platform.Platform));
            if(response.StatusCode == BusinessStatusCodeEnum.Success) { 
                return CreatedAtRoute(nameof(GetPlatformById), new { Id = response.Id }, platform);
            }
            else
            {
                return NotFound(response);
            }
        } 

        /// <summary>
        /// Delete platform.
        /// </summary>
        /// <param name="id">Platform identifier.</param>
        /// <returns>Return empty object.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeletePlatformResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlatform([FromRoute] int id) => Ok(await _platformService.DeletePlatformAsync(new(id)));
    }
}
