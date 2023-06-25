using simApi.Base;
using simApi.Data;
using simApi.Schema;

namespace simApi.Service;

public interface IOrderService:IBaseService<Order,OrderRequest,OrderResponse>
{
    Task<ApiResponse<IEnumerable<OrderResponse>>> GetUserOrders(string userId);
    Task<ApiResponse<IEnumerable<OrderResponse>>> GetOrdersIsActive();
    Task<ApiResponse<IEnumerable<OrderResponse>>> GetOrdersNumber(string orderNumber);
    Task<ApiResponse<IEnumerable<OrderResponse>>> GetOrdersIsPasif();



}
