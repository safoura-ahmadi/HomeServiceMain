
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
                new()  { Id = 1, Title = "دکوراسیون ساختمان", ImagePath = "icons8-sofa-50.png",IsActive = true},
                new() { Id = 2, Title = "تأسیسات ساختمان",ImagePath = "3.png",IsActive = true },
                new() { Id = 3, Title = "وسایل نقلیه",ImagePath = "1.png" , IsActive = true},
                new() { Id = 4, Title = "اسباب کشی و باربری",ImagePath = "icons8-truck-80.png" ,IsActive = true},
                new() { Id = 5, Title = "لوازم خانگی",ImagePath = "icons8-freezer-50.png",IsActive = true },
                new() { Id = 6, Title = "خدمات اداری",ImagePath = "icons8-chair-50.png", IsActive = true},
                new() { Id = 7, Title = "نظافت و بهداشت", ImagePath = "icons8-broom-50.png",IsActive = true},
                new() { Id = 8, Title = "دیجیتال و نرم افزار", ImagePath = "2.png",IsActive = true },

           });

    }
}
