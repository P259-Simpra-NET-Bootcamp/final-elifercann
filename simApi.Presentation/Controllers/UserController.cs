using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simApi.Base;
using simApi.Presentation.ActionFilters;
using simApi.Schema;
using simApi.Service;

namespace simApi.Presentation.Controllers;

[Route("simapi/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService service;
    private readonly IAuthenticationService authService;

    public UserController(IUserService service,IAuthenticationService authService)
    {
        this.service = service; 
        this.authService = authService;
    }

    [HttpGet]
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ApiResponse<List<AppUserResponse>>> GetAll()
    {
        var list = await service.GetAll();
        return list;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ApiResponse<AppUserResponse>> GetById(string id)
    {
        var model = await service.GetById(id);
        return model;
    }

    [HttpGet("GetUser")]
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ApiResponse<AppUserResponse>> GetUser()
    {
        var response = await service.GetUser(HttpContext.User);
        return response;
    }

    [HttpGet("GetUserId")]
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ApiResponse<string>> GetUserId()
    {
        var response = await service.GetUserId(HttpContext.User);
        return response;
    }
    [HttpGet("GetWallet")]
    [Authorize(Roles = "user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ApiResponse<string>> GetUserWallet()
    {
        var response = await service.GetWallet();
        return response;
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost("ChangePassword")]
    [Authorize(Roles = "user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ApiResponse> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var model = await authService.ChangePassword(HttpContext.User, request);
        return model;
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPut]
    [Authorize(Roles = "user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ApiResponse> Put([FromBody] AppUserRequest request)
    {
        var response = await service.Update(request);
        return response;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ApiResponse> Delete(string id)
    {
        var response = await service.Delete(id);
        return response;
    }


}