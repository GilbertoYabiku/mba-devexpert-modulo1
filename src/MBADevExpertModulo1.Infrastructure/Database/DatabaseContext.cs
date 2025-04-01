using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBADevExpertModulo1.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MBADevExpertModulo1.Infrastructure.Database;

public class DatabaseContext : DbContext
{
    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Seller> Seller { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Seller>(e =>
        {
            e.ToTable("Sellers");
            e.HasKey(e => e.Id);
            e.Property(e => e.Name).HasColumnType("NVARCHAR(100)").IsRequired();
            e.Property(e => e.Email).HasColumnType("VARCHAR(254)").IsRequired();
            e.Property(e => e.Password).HasColumnType("NVARCHAR(254)").IsRequired();
            e.Property(e => e.Deleted).HasColumnType("BOOL").HasDefaultValue(false).IsRequired();
        });

        modelBuilder.Entity<Category>(e =>
        {
            e.ToTable("Categories");
            e.HasKey(e => e.Id);
            e.Property(e => e.Name).HasColumnType("NVARCHAR(100)").IsRequired();
            e.Property(e => e.Description).HasColumnType("NVARCHAR(100)").IsRequired();
            e.Property(e => e.Deleted).HasColumnType("BOOL").HasDefaultValue(false).IsRequired();
        });

        modelBuilder.Entity<Product>(e =>
        {
            e.ToTable("Products");
            e.HasKey(e => e.Id);
            e.Property(e => e.Name).HasColumnType("NVARCHAR(100)").IsRequired();
            e.Property(e => e.Description).HasColumnType("NVARCHAR(100)").IsRequired();
            e.Property(e => e.Image).HasColumnType("VARBINARY(MAX)").IsRequired();
            e.Property(e => e.Price).IsRequired();
            e.Property(e => e.Stock).IsRequired();
            e.Property(e => e.Deleted).HasColumnType("BOOL").HasDefaultValue(false).IsRequired();
            e.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId).IsRequired();
            e.HasOne(e => e.Seller).WithMany(e => e.Products).HasForeignKey(e => e.SellerId).IsRequired();
        });
    }
}
