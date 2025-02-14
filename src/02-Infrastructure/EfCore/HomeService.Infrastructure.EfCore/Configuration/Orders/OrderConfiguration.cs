
using HomeService.Domain.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.Orders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Description)
            .HasMaxLength(255)
            .HasColumnType("nvarchar");

        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(o => o.Expert)
            .WithMany(e => e.CompletedOrders)
            .HasForeignKey(o => o.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(o => o.SubService)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.SubServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
            new Order()
            {
                Id = 1,

                IsActive = true,
                Price = 500000,
                CustomerId = 1,
                Status = Domain.Core.Enums.Orders.OrderStatusEnum.WaitingForExpertSelection,
                SubServiceId = 1,
                ImagePath = "Images/trending/2.jpg"

            });
    }

}

