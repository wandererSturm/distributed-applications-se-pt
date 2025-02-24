using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PS.WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("getuser")]
        public IActionResult Get()
        {
            var result = "SeedData.GetUsers()";
            return Ok(result);
        }
    }
}
