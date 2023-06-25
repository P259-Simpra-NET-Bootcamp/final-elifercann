using simApi.Base;
using System.ComponentModel.DataAnnotations;

namespace simApi.Schema;

public class OrderDetailRequest:BaseRequest
{
    [Required(ErrorMessage = "ProductId is a required field.")]

    public int ProductId { get; set; }
    [Required(ErrorMessage = "Quantity is a required field.")]

    public int Quantity { get; set; }

}
