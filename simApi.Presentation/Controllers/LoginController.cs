using Microsoft.AspNetCore.Mvc;
using simApi.Base;
using simApi.Presentation.ActionFilters;
using simApi.Schema;
using simApi.Service;

namespace simApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService service;
        private readonly IUserService userService;

        public LoginController(IAuthenticationService service, IUserService userService)
        {
            this.service = service;
            this.userService = userService;
        }

        [HttpPost("Login")]
        public async Task<ApiResponse<TokenResponse>> Login(TokenRequest request)
        {
            var list = await service.SignIn(request);
            return list;
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("Register")]
        public async Task<ApiResponse> Register([FromBody] AppUserRequest request)
        {
            var response = await userService.Insert(request);
            return response;
        }

    }
}
