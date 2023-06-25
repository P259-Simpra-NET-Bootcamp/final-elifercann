using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using simApi.Base;
using simApi.Data;
using simApi.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace simApi.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<AppUser> userManager;
    private readonly SignInManager<AppUser> signInManager;
    private readonly JwtConfig jwtConfig;
    private RoleManager<IdentityRole> roleManager;
    public AuthenticationService(UserManager<AppUser> userManager,
       SignInManager<AppUser> signInManager,
       IOptionsMonitor<JwtConfig> jwtConfig,
       RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.jwtConfig = jwtConfig.CurrentValue;
        this.roleManager = roleManager;
    }

    public async Task<ApiResponse<TokenResponse>> SignIn(TokenRequest request)
    {
        if (request is null)
        {
            return new ApiResponse<TokenResponse>("Request was null");
        }
        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
        {
            return new ApiResponse<TokenResponse>("Request was null");
        }

        var loginResult = await signInManager.PasswordSignInAsync(request.UserName, request.Password, true, false);
        if (!loginResult.Succeeded)
        {
            return new ApiResponse<TokenResponse>("Invalid user");
        }

        var user = await userManager.FindByNameAsync(request.UserName);

        string token = Token(user);
        TokenResponse tokenResponse = new TokenResponse
        {
            AccessToken = token,
            ExpireTime = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            UserName = user.UserName
        };

        return new ApiResponse<TokenResponse>(tokenResponse);
    }

    public async Task<ApiResponse> SignOut()
    {

        await signInManager.SignOutAsync();
        return new ApiResponse();
    }

    public async Task<ApiResponse> ChangePassword(ClaimsPrincipal User, ChangePasswordRequest request)
    {
        if (request is null)
        {
            return new ApiResponse("Request was null");
        }
        if (string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.OldPassword))
        {
            return new ApiResponse("Request was null");
        }

        var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value; // Kullanıcının kimlik bilgisini alın
        var user = await userManager.FindByIdAsync(userId); // Kullanıcıyı kimlik bilgisiyle bulun

        if (user == null)
        {
            return new ApiResponse("User not found");
        }

        var response = await userManager.ChangePasswordAsync(user, request.OldPassword, request.Password);
        if (!response.Succeeded)
        {
            return new ApiResponse("Change password error");
        }
        return new ApiResponse();
    }

    private string Token(AppUser user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }

    private Claim[] GetClaims(AppUser user)
    {
        var claims = new[]
        {
            new Claim("UserName",user.UserName),
            new Claim("UserId",user.Id.ToString()),
            new Claim("FirstName",user.FirstName),
            new Claim("LastName",user.LastName),
            new Claim("UserName",user.UserName),
            //new Claim("Roles",user.Roles),
            new Claim(ClaimTypes.Role,user.Roles),

        };

        return claims;
    }
   

}
