using simApi.Base;
using simApi.Data;

namespace simApi.Schema;

public class OrderDetailResponse:BaseResponse
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
