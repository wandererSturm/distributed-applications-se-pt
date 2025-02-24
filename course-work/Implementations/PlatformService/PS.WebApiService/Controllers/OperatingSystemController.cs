using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PS.ApplicationServices.Interfaces;
using PS.ApplicationServices.Messaging;
using PS.Infrastructure.Messaging.Responses.OperatingSystem;
using PS.Infrastructure.Messaging.Requests.OperatingSystem;
using PS.Repositories.Interfaces;
using PS.ApplicationServices.Messaging.Responses.OperatingSystem;

namespace PS.WebApiService.Controllers
{
    /// <summary>
    /// OperatingSystem controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    [Produces("application/json")]
    public class OperatingSystemController : ControllerBase
    {
        private readonly IOperatingSystemManagementService _operatingSystemService;

        /// <summary>
        /// OperatingSystem controller object
        /// </summary>
        /// <param name="OperatingSystemService"></param>
        public OperatingSystemController(IOperatingSystemManagementService operatingSystemService)
        {
            _operatingSystemService = operatingSystemService;
        }

        /// <summary>
        /// Get Operating system
        /// </summary>
        /// <returns>Return list of Operating systems.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetOperatingSystemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status404NotFound)]
        [HttpGet("{id}", Name = "GetOperatingSystemById")]
        public async Task<IActionResult> GetOperatingSystemById(int id)
        {
            if(id == 0)
            {
                return Ok(await _operatingSystemService.GetOperatingSystemAsync(new( -1)));
            }
            return Ok(await _operatingSystemService.GetOperatingSystemByIdAsync(new(id)));
        }


        /// <summary>
        /// Create new Operating system.
        /// </summary>
        /// <param name="platform">Operating system model object.</param>
        /// <returns>Return create empty response.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateOperatingSystemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]        
        public async Task<IActionResult> CreateOperatingSystem([FromBody] OperatingSystemModel operatingSystem)
        {
           var res =  await _operatingSystemService.CreateOperatingSystemAsync(new(operatingSystem));
            return CreatedAtRoute(nameof(GetOperatingSystemById), new { Id = res.Id }, operatingSystem);            
        } 

        /// <summary>
        /// Delete operating system.
        /// </summary>
        /// <param name="id">Operating system identifier.</param>
        /// <returns>Return empty object.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteOperatingSystemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOperatingSystem([FromRoute] int id) => Ok(await _operatingSystemService.DeleteOperatingSystemAsync(new(id)));
    }
}
