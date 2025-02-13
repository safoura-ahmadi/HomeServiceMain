
using HomeService.Domain.Core.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(55)
            .HasColumnType("nvarchar");

        builder.HasData(new List<Category>()
            {
                new Category() { Id = 1, Title = "دکوراسیون ساختمان", ImagePath = "\\UserTemplate\\images\\icon\\1.png",IsActive = true},
                new Category() { Id = 2, Title = "تأسیسات ساختمان",ImagePath = "\\UserTemplate\\images\\icon\\2.png",IsActive = true },
                new Category() { Id = 3, Title = "وسایل نقلیه",ImagePath = "\\UserTemplate\\images\\icon\\3.png" , IsActive = true},
                new Category() { Id = 4, Title = "اسباب کشی و باربری",ImagePath = "\\UserTemplate\\images\\icon\\7.png" ,IsActive = true},
                new Category() { Id = 5, Title = "لوازم خانگی",ImagePath = "\\UserTemplate\\images\\icon\\8.png",IsActive = true },
                new Category() { Id = 6, Title = "خدمات اداری",ImagePath = "\\UserTemplate\\images\\icon\\6.png", IsActive = true},
                new Category() { Id = 7, Title = "نظافت و بهداشت", ImagePath = "\\UserTemplate\\images\\icon\\7.png",IsActive = true},
                new Category() { Id = 8, Title = "دیجیتال و نرم افزار", ImagePath = "\\UserTemplate\\images\\icon\\8.png",IsActive = true },

           });

    }
}
