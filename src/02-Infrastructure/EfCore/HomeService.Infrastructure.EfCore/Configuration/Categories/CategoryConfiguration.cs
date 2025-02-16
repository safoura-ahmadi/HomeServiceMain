
using HomeService.Domain.Core.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        //  "\\UserTemplate\\images\\icon\\7.png

        builder.HasData(new List<Category>()
            {
                new()  { Id = 1, Title = "دکوراسیون ساختمان", ImagePath = "1.png",IsActive = true},
                new() { Id = 2, Title = "تأسیسات ساختمان",ImagePath = "2.png",IsActive = true },
                new() { Id = 3, Title = "وسایل نقلیه",ImagePath = "3.png" , IsActive = true},
                new() { Id = 4, Title = "اسباب کشی و باربری",ImagePath = "7.png" ,IsActive = true},
                new() { Id = 5, Title = "لوازم خانگی",ImagePath = "8.png",IsActive = true },
                new() { Id = 6, Title = "خدمات اداری",ImagePath = "6.png", IsActive = true},
                new() { Id = 7, Title = "نظافت و بهداشت", ImagePath = "7.png",IsActive = true},
                new() { Id = 8, Title = "دیجیتال و نرم افزار", ImagePath = "8.png",IsActive = true },

           });

    }
}
