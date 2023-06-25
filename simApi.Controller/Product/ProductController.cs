using Microsoft.AspNetCore.Mvc;
using simApi.Controller.ActionFilter;
using simApi.Schema;
using simApi.Services;

namespace simApi.Controller.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _services;

        public ProductController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductControllerAsync()
        {
            return Ok(await _services
                .ProductService.GetAllProductAsync(false));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllProductAsync([FromRoute] int id)
        {
            return Ok(await _services
                .ProductService.GetOneProductByIdAsync(id, false));
        }

        [HttpGet("GetOne/{id:int}")]
        public async Task<IActionResult> GetOneProductAsync([FromRoute(Name = "id")] int id)
        {
            var product = await _services.ProductService.GetOneProductByIdAsync(id, false);

            return Ok(product);
        }

        //[Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateOneProductAsync")]
        public async Task<IActionResult> CreateOneProductAsync([FromBody] ProductRequest request)
        {
            var product = await _services.ProductService.CreateOneProduct(request);
            return StatusCode(201, product); // CreatedAtRoute()
        }

        //[Authorize(Roles = "Editor, Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneProductAsync([FromRoute(Name = "id")] int id, [FromBody] ProductRequest request)
        {
            await _services.ProductService.UpdateOneProduct(id, false, request);
            return NoContent(); // CreatedAtRoute()
        }
        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneProductAsync([FromRoute(Name = "id")] int id)
        {
            await _services.ProductService.DeleteOneProduct(id, false);
            return NoContent();
        }
    }
}
