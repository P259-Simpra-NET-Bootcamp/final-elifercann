using simApi.Data;

namespace simApi.Schema;

public class AppUserResponse:AppUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
}
