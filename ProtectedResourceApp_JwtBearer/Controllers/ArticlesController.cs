using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProtectedResourceApp_JwtBearer.Controllers
{
    public class ArticlesController : Controller
    {
        [Authorize]
        [HttpGet("GetDummyData")]
        public IActionResult GetDummyData()
        {
            string result = "Calling to this endpoint at https://localhost:7065 is done successfully";
            return Ok(result);
        }
    }
}
