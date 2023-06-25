using Autofac;
using simApi.Services;

namespace simApi.Controller.Autofac;

public class AutofacBussinesModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {

        //builder.RegisterType<UserLogService>().As<IUserLogService>().InstancePerLifetimeScope();
        //builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
        //builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        //builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
        //builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
        //builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
        //builder.RegisterType<OrderDetailService>().As<IOrderDetailService>().InstancePerLifetimeScope();
        //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();



        builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
        builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
        builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
        builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
        builder.RegisterType<LoggerManager>().As<ILoggerService>().InstancePerLifetimeScope();
        builder.RegisterType<ServiceManager>().As<IServiceManager>().InstancePerLifetimeScope();



    }

}