
using HomeService.Domain.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.Orders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {


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
                Id = 2,

                IsActive = true,
                Price = 500000,
                CustomerId = 1,
                Status = Domain.Core.Enums.Orders.OrderStatusEnum.WaitingForExpertSelection,
                SubServiceId = 1,

            }, new Order()
            {
                Id = 1,
                IsActive = true,
                Price = 500000,
                CustomerId = 1,
                Status = Domain.Core.Enums.Orders.OrderStatusEnum.WorkCompletedAndPaid,
                SubServiceId = 1,
                ExpertId = 1

            }


            );

    }

}

