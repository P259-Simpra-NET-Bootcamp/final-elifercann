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
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [Authorize(Roles = "admin,user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse<List<ProductResponse>> GetAll()
        {
            var productList = productService.GetAll();
            return productList;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse<ProductResponse> GetById([FromRoute] int id)
        {
            var product = productService.GetById(id);
            return product;
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse Post([FromBody] ProductRequest request)
        {
            return productService.Insert(request);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id}")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse Put([FromRoute] int id, [FromBody] ProductRequest request)
        {
            return productService.Update(id, request);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse Delete([FromRoute] int id)
        {
            return productService.Delete(id);
        }
    }
}
