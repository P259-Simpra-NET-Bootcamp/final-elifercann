using simApi.Data;
using simApi.Schema;

namespace simApi.Services;

public interface IOrderService
{
    Task<List<OrderResponse>> GetOrders(bool trackChanges);
    Task<List<OrderResponse>> GetUserOrders(bool trackChanges, int userId);
}
