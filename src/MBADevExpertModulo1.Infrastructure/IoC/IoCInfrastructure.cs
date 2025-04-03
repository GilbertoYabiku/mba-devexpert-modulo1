using MBADevExpertModulo1.Infrastructure.Interfaces;
using MBADevExpertModulo1.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MBADevExpertModulo1.Infrastructure.IoC;

public static class IoCInfrastructure
{
    public static void InitializeInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISellerRepository, SellerRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}

