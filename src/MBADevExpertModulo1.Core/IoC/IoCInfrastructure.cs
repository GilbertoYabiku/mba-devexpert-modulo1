using MBADevExpertModulo1.Core.Interfaces;
using MBADevExpertModulo1.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MBADevExpertModulo1.Core.IoC;

public static class IoCInfrastructure
{
    public static void InitializeInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISellerRepository, SellerRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}

