using HomeService.Domain.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.Orders;

public class SuggestionConfiguration : IEntityTypeConfiguration<Suggestion>
{
    public void Configure(EntityTypeBuilder<Suggestion> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Description)
            .HasMaxLength(255)
            .HasColumnType("nvarchar");

        builder.HasOne(s => s.Expert)
            .WithMany(e => e.Suggestions)
            .HasForeignKey(e => e.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(s => s.Order)
            .WithMany(o => o.Suggestions)
            .HasForeignKey(s => s.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(

            new Suggestion()
            {
                Id = 1,
                OrderId = 1,
                Price = 505000,
                ExpertId = 1,
                IsAccepted = false,
                               

            }

            );

    }
}
