using simApi.Base;
using System.ComponentModel.DataAnnotations;

namespace simApi.Schema;

public class PaymentRequest:BaseRequest
{
    [Required(ErrorMessage = "OrderNumber is a required field.")]
    [MaxLength(9, ErrorMessage = "OrderNumber must consist of at maximum 9 caharacters")]

    public string OrderNumber { get; set; }
    [Required(ErrorMessage = "TotalAmount is a required field.")]

    public decimal TotalAmount { get; set; }
    [Required(ErrorMessage = "CouponAmount is a required field.")]

    public decimal CouponAmount { get; set; }
    [Required(ErrorMessage = "PointNumber is a required field.")]

    public decimal PointAmount { get; set; }
    [Required(ErrorMessage = "Code is a required field.")]
    [MaxLength(10, ErrorMessage = "Code must consist of at maximum 10 caharacters")]
    public string CouponCode { get; set; }
}
