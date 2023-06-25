using Microsoft.AspNetCore.Identity;

namespace simApi.Data;

public class AppUser:IdentityUser
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal DigitalWallet { get; set; }
    public string Roles { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}
