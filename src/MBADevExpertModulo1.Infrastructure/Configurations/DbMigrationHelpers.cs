using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using MBADevExpertModulo1.Domain.Models;
using MBADevExpertModulo1.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MBADevExpertModulo1.Infrastructure.Configurations;

public static class DbMigrationHelpersExtension
{
    public static void UseDbMigrationHelper(this WebApplication app)
    {
        DbMigrationHelpers.EnsureSeedData(app).Wait();
    }
}

public static class DbMigrationHelpers
{
    public static async Task EnsureSeedData(WebApplication serviceScope)
    {
        var services = serviceScope.Services.CreateScope().ServiceProvider;
        await EnsureSeedData(services);
    }

    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        if(env.IsDevelopment() || env.IsEnvironment("Docker"))
        {
            await context.Database.MigrateAsync();
            await EnsureSeedPopulates(context);
        }
    }

    private static async Task EnsureSeedPopulates(DatabaseContext databaseContext)
    {
        if (databaseContext.Seller.Any()) return;

        var sellerId = new Random().Next(1000);

        await databaseContext.Seller.AddAsync(new Seller()
        {
            Id = sellerId,
            Name = "seller 1 name",
            Email = "email@email.com",
            Deleted = false,
            Products = new List<Product>(){ 
                new Product(){
                    Id =new Random().Next(1000),
                    Name = "product 1 name",
                    SellerId = sellerId,
                    Description = "description",
                    Price = new Random().Next(1000),
                    Stock = new Random().Next(1000),
                    Image = [0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff],
                    Deleted = false,
                    Category = new Category() {
                        Id = new Random().Next(1000),
                        Name = "category 1 name",
                        Description = "category 1 description",
                        Deleted = false
                    }
                },
                new Product(){
                    Id =new Random().Next(1000),
                    Name = "product 2 name",
                    SellerId = sellerId,
                    Description = "description",
                    Price = new Random().Next(1000),
                    Stock = new Random().Next(1000),
                    Image = [0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff],
                    Deleted = false,
                    Category = new Category() {
                        Id = new Random().Next(1000),
                        Name = "category 2 name",
                        Description = "category 2 description",
                        Deleted = false
                    }
                }
            }
        });

        await databaseContext.SaveChangesAsync();

        if (databaseContext.Users.Any()) return;

        await databaseContext.Users.AddAsync(new IdentityUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "user1@email.com",
            NormalizedUserName = "USER1@EMAIL.COM",
            Email = "user1@email.com",
            NormalizedEmail = "USER1@EMAIL.COM",
            AccessFailedCount = 0,
            LockoutEnabled = false,
            PasswordHash = "AQAAAAIAAYagAAAAEEdWhqiCwW/jZz0hEM7aNjok7IxniahnxKxxO5zsx2TvWs4ht1FUDnYofR8JKsA5UA==",
            TwoFactorEnabled = false,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        });

        await databaseContext.SaveChangesAsync();
    }
}