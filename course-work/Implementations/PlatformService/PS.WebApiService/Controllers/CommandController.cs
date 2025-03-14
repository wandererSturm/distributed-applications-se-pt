namespace PS.WebApiService.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PS.ApplicationServices.Interfaces;
    using PS.ApplicationServices.Messaging;
    using PS.Infrastructure.Messaging.Responses.Commands;
    using PS.Infrastructure.Messaging.Requests.Commands;

    /// <summary>
    /// Command controller
    /// </summary>
    /// <param name="cmdMgmtService">ICommandManagementService DI</param>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    [Produces("application/json")]
    public class CommandController(ICommandManagementService cmdMgmtService) : ControllerBase
    {
        private readonly ICommandManagementService _cmdMgmtService = cmdMgmtService;

        /// <summary>
        /// Create new command.
        /// </summary>
        /// <param name="platform">Command model object.</param>
        /// <returns>Return create empty response.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCommand([FromBody] CreateCommandRequest request)
        {
            var response = await _cmdMgmtService.CreateCommandAsync(new(request.Command));
            if (response.StatusCode == BusinessStatusCodeEnum.Success)
            {
                return CreatedAtRoute(nameof(GetCommandById), new { Id = response.Id }, request);
            }
            else
            {
                return NotFound(response);
            }
        }

        /// <summary>
        /// Get command by Id
        /// </summary>
        /// <param name="id">Id of desired command</param>
        /// <returns>Returns the command if found. Otherwise returns bad request.</returns>
        [HttpGet("{id}", Name = "GetCommandById")]
        [ProducesResponseType(typeof(GetCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCommandById(int id)
        {
            if (id == 0)
            {
                return Ok(await _cmdMgmtService.GetCommandAsync(new(-1)));
            }
            return Ok(await _cmdMgmtService.GetCommandByIdAsync(new(id)));
        }


        /// <summary>
        /// Get all commands
        /// </summary>
        /// <returns>Returns all commands. Otherwise returns bad request.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCommandAsync()
        {
            return Ok(await _cmdMgmtService.GetCommandAsync(new(-1)));
        }

        /// <summary>
        /// Delete command.
        /// </summary>
        /// <param name="id">Command identifier.</param>
        /// <returns>Return empty object.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCommand([FromRoute] int id) 
            => Ok(await _cmdMgmtService.DeleteCommandAsync(new(id)));
    }
}
