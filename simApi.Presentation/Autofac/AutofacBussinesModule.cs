using Autofac;
using simApi.Data.UnitOfWork;
using simApi.Service;

namespace simApi.Presentation.Autofac;

public class AutofacBussinesModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {

        builder.RegisterType<UserLogService>().As<IUserLogService>().InstancePerLifetimeScope();
        builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
        builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
        builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
        builder.RegisterType<OrderDetailService>().As<IOrderDetailService>().InstancePerLifetimeScope();
        builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
        builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        builder.RegisterType<CouponService>().As<ICouponService>().InstancePerLifetimeScope();
        builder.RegisterType<PaymentService>().As<IPaymentService>().InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    }

}