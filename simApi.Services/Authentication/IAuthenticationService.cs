using Microsoft.AspNetCore.Identity;
using simApi.Schema;

namespace simApi.Services;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserForRegisterRequest userForRegistrationDto);
    Task<bool> ValidateUser(UserForAuthenticationResponse userForAuthDto);
    Task<TokenDto> CreateToken(bool populateExp);
    Task<TokenDto> RefreshToken(TokenDto tokenDto);
}
