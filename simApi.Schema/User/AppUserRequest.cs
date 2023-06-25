using System.ComponentModel.DataAnnotations;

namespace simApi.Schema;

public class AppUserRequest
{

    [Required(ErrorMessage = "FirstName is a required field.")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "LastName is a required field.")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "UserName is a required field.")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Email is a required field.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is a required field.")]
    public string Password { get; set; }

}
