using System.ComponentModel.DataAnnotations;

namespace simApi.Schema;

public class ChangePasswordRequest
{
    [Required(ErrorMessage = "OldPassword is a required field.")]

    public string OldPassword { get; set; }
    [Required(ErrorMessage = "Password is a required field.")]

    public string Password { get; set; }
}