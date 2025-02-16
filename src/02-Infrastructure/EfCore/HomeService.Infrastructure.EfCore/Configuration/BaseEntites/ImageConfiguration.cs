using HomeService.Domain.Core.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.BaseEntites;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasOne(i => i.Order)
                .WithMany(o => o.Images)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new List<Image>()
            {
                new() { Id = 1, OrderId = 1, Path = "Images/trending/1.jpg" },
                new() { Id = 2, OrderId = 1, Path = "Images/trending/2.jpg" }
             
            });
    }
}
