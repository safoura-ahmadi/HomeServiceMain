
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Enums.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.BaseEntites;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {

        builder.HasOne(c => c.Expert)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.ExpertId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.Customer)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);


        builder.HasData(
            new Comment()
            {
                Id = 1,
                ExpertId = 1,
                CustomerId = 1,
               Status = CommentStatusEnum.Pending,
                Text = "کارشون عالیه",
                Score = 8
            }
            );
    }
}
