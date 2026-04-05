using Checkout.API.Domain.Entities;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Checkout.API.Infrastructure.Data;

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
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.TotalAmount)
                .HasPrecision(18, 2);
        });

        modelBuilder.Entity<OutboxMessage>(builder =>
        {
            builder.ToTable(nameof(OutboxMessage).Pluralize());
            builder.HasKey(x => x.Id);
        });
    }
}