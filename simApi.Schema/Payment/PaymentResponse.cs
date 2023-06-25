using simApi.Base;

namespace simApi.Schema;

public class PaymentResponse:BaseResponse
{
    public string OrderNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime PaymentDate { get; set; }
}
