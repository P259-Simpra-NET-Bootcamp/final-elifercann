using AutoMapper;
using simApi.Data;

namespace simApi.Schema;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Category, CategoryResponse>();
        CreateMap<CategoryRequest, Category>();

        CreateMap<Product, ProductResponse>();
        CreateMap<ProductRequest, Product>();


        CreateMap<UserLogRequest, UserLog>();
        CreateMap<UserLog, UserLogResponse>();

        CreateMap<OrderRequest, Order>();
        CreateMap<Order, OrderResponse>();

        CreateMap<OrderDetailRequest, OrderDetail>();
        CreateMap<OrderDetail, OrderDetailResponse>();


        CreateMap<Coupon, CouponResponse>();
        CreateMap<CouponRequest, Coupon>();

        CreateMap< AppUser,AppUserResponse>();
        CreateMap<AppUserRequest,AppUser>();

     


    }
}
