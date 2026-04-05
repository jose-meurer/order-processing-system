using Checkout.API.Domain.Entities;
using Checkout.API.Infrastructure.Data.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Checkout.API.Infrastructure.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Order>(builder =>
        {
            builder.ToTable(nameof(Order).Pluralize());
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Status)
                .HasMaxLength(75)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.CustomerEmail)
                .HasMaxLength(255)
                .IsRequired();
            
            builder.Property(x => x.TotalAmount)
                .HasPrecision(18, 2);
        });

        modelBuilder.Entity<OutboxMessage>(builder =>
        {
            builder.ToTable(nameof(OutboxMessage).Pluralize());
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.ErrorMessage)
                .HasMaxLength(255)
                .IsRequired();
            
            builder.Property(x => x.CreatedAt)
                .HasColumnType("jsonb")
                .IsRequired();
        });
    }
}