using AutoMapper;
using simApi.Data;
using simApi.Data.Repository;
using simApi.Schema;

namespace simApi.Services;

public class OrderService : IOrderService
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;

    public OrderService(IRepositoryManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<List<OrderResponse>> GetOrders(bool trackChanges)
    {
        var orders=await _manager.Order.GetOrders(trackChanges);
        var mapped=_mapper.Map<List<Order>,List<OrderResponse>>(orders);
        return mapped;
       
    }

    public async Task<List<OrderResponse>> GetUserOrders(bool trackChanges, int userId)
    {
        var userOrders=await _manager.Order.GetUserOrders(trackChanges, userId);
        var mapped=_mapper.Map<List<Order>, List<OrderResponse>>(userOrders);
        return mapped;
    }
}
