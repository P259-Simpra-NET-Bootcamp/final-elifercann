using Microsoft.AspNetCore.Mvc;
using simApi.Controller.ActionFilter;
using simApi.Schema;
using simApi.Services;

namespace simApi.Controller.Category
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IServiceManager _services;

        public CategoriesController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            return Ok(await _services
                .CategoryService
                .GetAllCategoriesAsync(false));
        }

        [HttpGet("GetAll/{id:int}")]
        public async Task<IActionResult> GetAllCategoriesAsync([FromRoute] int id)
        {
            return Ok(await _services
                .CategoryService
                .GetOneCategoryByIdAsync(id, false));
        }
        
        [HttpGet("GetOne/{id:int}")]
        public async Task<IActionResult> GetOneCategoryAsync([FromRoute(Name = "id")] int id)
        {
            var category = await _services
            .CategoryService
            .GetOneCategoryByIdAsync(id, false);

            return Ok(category);
        }

        //[Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateOneCategoryAsync")]
        public async Task<IActionResult> CreateOneCategoryAsync([FromBody] CategoryRequest request)
        {
            var category = await _services.CategoryService.CreateOneCategory(request);
            return StatusCode(201, category); // CreatedAtRoute()
        }

        //[Authorize(Roles = "Editor, Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneCategoryAsync([FromRoute(Name = "id")] int id, [FromBody] CategoryRequest request)
        {
            await _services.CategoryService.UpdateOneCategory(id,false,request);
            return NoContent(); // CreatedAtRoute()
        }
        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneCategoryAsync([FromRoute(Name = "id")] int id)
        {
            await _services.CategoryService.DeleteOneCategory(id, false);
            return NoContent();
        }
    }
}
