
using HomeService.Domain.Core.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.BaseEntites;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Text)
            .HasMaxLength(255);

        builder.HasOne(c => c.Expert)
            .WithMany(e => e.Comments)
            .HasForeignKey(e => e.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.Customer)
            .WithMany(c => c.Comments)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
