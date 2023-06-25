using simApi.Base;
using System.ComponentModel.DataAnnotations;

namespace simApi.Schema;

public class OrderRequest:BaseRequest
{
   
    [MaxLength(10, ErrorMessage = "Code must consist of at maximum 10 caharacters")]
    public string CouponCode { get; set; }
  
    [Required(ErrorMessage = "Order Detail is a required field.")]

    public List<OrderDetailRequest> OrderDetails { get; set; }
}
