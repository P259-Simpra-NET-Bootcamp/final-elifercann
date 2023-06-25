using simApi.Base;
using simApi.Schema;
using System.Security.Claims;

namespace simApi.Service;

public interface IAuthenticationService
{
    public Task<ApiResponse<TokenResponse>> SignIn(TokenRequest request);
    public Task<ApiResponse> SignOut();
    public Task<ApiResponse> ChangePassword(ClaimsPrincipal User, ChangePasswordRequest request);
}
