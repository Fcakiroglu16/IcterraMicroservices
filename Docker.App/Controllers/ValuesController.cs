using Docker.App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Docker.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ProductService _productService;

        public ValuesController(ProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var response = await _productService.GetList();

            return Ok(response);
        }
    }
}
