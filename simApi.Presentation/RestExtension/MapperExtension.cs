using AutoMapper;
using simApi.Schema;

namespace simApi.Presentation.RestExtension;

public static class MapperExtension
{
    public static void AddMapperExtension(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperProfile());
        });
        services.AddSingleton(config.CreateMapper());
    }
}