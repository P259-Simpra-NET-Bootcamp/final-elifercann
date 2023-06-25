using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simApi.Base;
using simApi.Presentation.ActionFilters;
using simApi.Schema;
using simApi.Service;

namespace simApi.Presentation.Controllers
{

    [Route("simapi/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet()]
        [Authorize(Roles = "admin,user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse<List<CategoryResponse>> GetAllBasic()
        {
            var categoryList =  categoryService.GetAll();

            return categoryList;
        }

        [HttpGet("{id}")]
        [Authorize(Roles="admin,user",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse<CategoryResponse> GetById([FromRoute] int id)
       {
            var category = categoryService.GetById(id);
            return category;
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public ApiResponse Post([FromBody] CategoryRequest request)
        {
            return categoryService.Insert(request);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id}")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse Put([FromRoute] int id, [FromBody] CategoryRequest request)
        {
            return categoryService.Update(id, request);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse Delete([FromRoute] int id)
        {
            return categoryService.Delete(id);
        }
    }
}
