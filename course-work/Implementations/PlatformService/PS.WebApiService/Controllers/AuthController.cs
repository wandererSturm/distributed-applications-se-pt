using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PS.ApplicationServices.Interfaces;
using PS.Infrastructure.Messaging.Requests;
using PS.Infrastructure.Messaging.Responses;
using PS.Infrastructure.Messaging.Responses.Authentications;
using PS.Infrastructure.Messaging.Responses.Platforms;

namespace PS.WebApiService.Controllers
{
    /// <summary>
    /// Authentication controller.
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="jwtAuthenticationManager">Jwt authentication manager</param>
        public AuthController(IJwtAuthenticationManager jwtAuthenticationManager, IAuthService authService)
        {
            _authService = authService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        ///// <summary>
        ///// Generate Jwt token.
        ///// </summary>
        ///// <param name="clientId">Client identifier</param>
        ///// <param name="secret">Clent secret</param>
        ///// <returns></returns>
        //[HttpGet]
        //[ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //public async Task<IActionResult> Token([FromQuery] string clientId, [FromQuery] string secret)
        //{
        //    string? token = _jwtAuthenticationManager.Authenticate(clientId, secret);

        //    if(token == null)
        //    {
        //        return Unauthorized();
        //    }
        //    return Ok(await Task.FromResult(new AuthenticationResponse(token)));
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Route("CleintLogin")]
        public async Task<IActionResult> ClientLogin(string userName, string password)
        {
            var res = await _authService.Login(userName, password);
            if (res.Token.Length == 0)
            {
                return Unauthorized();
            }
            return Ok(await Task.FromResult(new AuthenticationResponse(res.Token, res.Id, res.Email)));
        }

        [HttpPut]
        [ProducesResponseType(typeof(RegistrationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Route("CleintRegister")]
        public async Task<IActionResult> ClientRegister(RegistrationRequest request, string role)
        {
            var res = await _authService.RegistrationAsync(request, role);
           
            return Ok(await Task.FromResult(res));
        }
    }
}

