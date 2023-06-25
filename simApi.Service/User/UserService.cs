using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using simApi.Base;
using simApi.Data;
using simApi.Schema;
using System.Security.Claims;

namespace simApi.Service;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> userManager;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor contextAccessor;

    public UserService(UserManager<AppUser> userManager, IMapper mapper,IHttpContextAccessor contextAccessor)
    {
        this.userManager = userManager;
        this.mapper = mapper;
        this.contextAccessor = contextAccessor;
    }

    public async Task<ApiResponse<AppUserResponse>> GetUser(ClaimsPrincipal User)
    {
        var user = await userManager.GetUserAsync(User);
        var mapped = mapper.Map<AppUserResponse>(user);
        return new ApiResponse<AppUserResponse>(mapped);
    }
    public async Task<ApiResponse> Insert(AppUserRequest request)
    {
        if (request is null)
        {
            return new ApiResponse("Request was null");
        }
        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Email))
        {
            return new ApiResponse("Request was null");
        }

        var entity = mapper.Map<AppUser>(request);
        entity.EmailConfirmed = true;
        entity.TwoFactorEnabled = false;
        entity.Roles = "user";

        try
        {
            var response = await userManager.CreateAsync(entity, request.Password);
            if (!response.Succeeded)
            {
                return new ApiResponse(response.Errors.FirstOrDefault()?.Description);
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse($"An error occurred while creating the user: {ex.Message}");
        }

        var roleResult = await userManager.AddToRoleAsync(entity, entity.Roles);
        if (!roleResult.Succeeded)
        {
            return new ApiResponse(roleResult.Errors.FirstOrDefault()?.Description);
        }

        return new ApiResponse();
    }
    public async Task<ApiResponse> Update(AppUserRequest request)
    {
        if (request is null)
        {
            return new ApiResponse("Request was null");
        }
        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Email))
        {
            return new ApiResponse("Request was null");
        }

        var mapped = mapper.Map<AppUser>(request);
        await userManager.UpdateAsync(mapped);

        return new ApiResponse();
    }

    public async Task<ApiResponse> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return new ApiResponse("Request was null");
        }

        var user = userManager.Users.Where(x => x.Id == id).FirstOrDefault();
        await userManager.DeleteAsync(user);

        return new ApiResponse();
    }

    public async Task<ApiResponse<List<AppUserResponse>>> GetAll()
    {
        var list = userManager.Users.ToList();
        var mapped = mapper.Map<List<AppUserResponse>>(list);
        return new ApiResponse<List<AppUserResponse>>(mapped);
    }
    public async Task<ApiResponse<AppUserResponse>> GetById(string id)
    {
        var list = userManager.Users.Where(x => x.Id == id).FirstOrDefault();
        var mapped = mapper.Map<AppUserResponse>(list);
        return new ApiResponse<AppUserResponse>(mapped);
    }
    public async Task<ApiResponse<string>> GetUserId(ClaimsPrincipal User)
    {
        var id = userManager.GetUserId(User);
        return new ApiResponse<string>(id);
    }

    public async Task<ApiResponse<string>> GetWallet()
    {
        var httpContext = contextAccessor.HttpContext;
        var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        var userId = userIdClaim?.Value;
        var userWallet=userManager.Users.Where(x=>x.Id == userId).FirstOrDefault();
        var wallet = userWallet.DigitalWallet.ToString();
        return new ApiResponse<string> (wallet);

    }

}