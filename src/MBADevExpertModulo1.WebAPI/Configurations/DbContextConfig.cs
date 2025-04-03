using MBADevExpertModulo1.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MBADevExpertModulo1.WebAPI.Configurations;

public static class DbContextConfig
{
    public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        return builder;
    }
}
