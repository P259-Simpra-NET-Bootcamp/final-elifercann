using simApi.Base;

namespace simApi.Schema;

public class OrderResponse:BaseResponse
{
    public DateTime OrderDate { get; set; }
    public bool IsActive { get; set; }

}
