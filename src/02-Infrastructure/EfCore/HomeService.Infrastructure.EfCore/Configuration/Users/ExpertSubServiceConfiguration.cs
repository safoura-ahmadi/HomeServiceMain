
using HomeService.Domain.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.Users;

public class ExpertSubServiceConfiguration : IEntityTypeConfiguration<ExpertSubService>
{
    public void Configure(EntityTypeBuilder<ExpertSubService> builder)
    {

        builder.HasKey(x => new { x.ExpertId, x.SubServiceId });
        builder.HasOne(x => x.SubService)
            .WithMany(x => x.ExpertSubServices)
            .HasForeignKey(x => x.SubServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Expert)
            .WithMany(x => x.ExpertSubServices)
            .HasForeignKey(x => x.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
            new ExpertSubService()
            {
                ExpertId = 1,
                SubServiceId = 1
            }

            );


    }
}
