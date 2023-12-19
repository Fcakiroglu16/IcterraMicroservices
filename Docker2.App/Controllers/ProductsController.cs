using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Docker2.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(new List<string>() { "kalem 1", "kalem 2" });
        }
    }
}
