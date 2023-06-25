using simApi.Base;
using System.ComponentModel.DataAnnotations;

namespace simApi.Schema;

public class CouponRequest:BaseRequest
{
    [Required(ErrorMessage = "Code is a required field.")]
    [MaxLength(10, ErrorMessage = "Code must consist of at maximum 10 caharacters")]
    public string Code { get; set; }
    [Required(ErrorMessage = "Amount is a required field.")]
    public decimal Amount { get; set; }
    [Required(ErrorMessage = "ExpiryDate is a required field.")]
    public DateTime ExpiryDate { get; set; }
    public bool isActive { get; set; }

}
