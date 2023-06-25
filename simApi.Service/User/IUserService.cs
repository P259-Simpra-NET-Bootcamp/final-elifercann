using simApi.Base;
using simApi.Schema;
using System.Security.Claims;

namespace simApi.Service;

public interface IUserService
{
    public Task<ApiResponse<AppUserResponse>> GetUser(ClaimsPrincipal User);
    public Task<ApiResponse> Insert(AppUserRequest request);
    public Task<ApiResponse> Update(AppUserRequest request);
    public Task<ApiResponse> Delete(string id);
    public Task<ApiResponse<List<AppUserResponse>>> GetAll();
    public Task<ApiResponse<AppUserResponse>> GetById(string id);
    public Task<ApiResponse<string>> GetWallet();
    public Task<ApiResponse<string>> GetUserId(ClaimsPrincipal User);
}