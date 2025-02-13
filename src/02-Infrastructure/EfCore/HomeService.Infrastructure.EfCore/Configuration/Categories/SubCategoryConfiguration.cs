
using HomeService.Domain.Core.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.Categories;

public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Title)
            .IsRequired()
            .HasMaxLength(55)
            .HasColumnType("nvarchar");


        builder.HasOne(sc => sc.Category)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(sc => sc.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasData(

           new SubCategory()
           {
               Id = 1,
               Title = "بنایی",
               CategoryId = 1,

           }, new SubCategory()
           {
               Id = 2,
               Title = "دکوراسیون",
               CategoryId = 1,

           }, new SubCategory()
           {
               Id = 3,
               Title = "نقاشی ساختمان",
               CategoryId = 1,

           }, new SubCategory()
           {
               Id = 4,
               Title = "درب و پنجره",
               CategoryId = 1,

           }, new SubCategory()
           {
               Id = 5,
               Title = "اهنگری و جوشکاری",
               CategoryId = 1,

           }, new SubCategory()
           {
               Id = 6,
               Title = "باغبانی",
               CategoryId = 1,

           },
            new SubCategory()
            {
                Id = 7,
                Title = "سرمایش گرمایش",
                CategoryId = 2,

            }, new SubCategory()
            {
                Id = 8,
                Title = "لوله کشی",
                CategoryId = 2,

            }, new SubCategory()
            {
                Id = 9,
                Title = "برق و الکترونیک",
                CategoryId = 2,

            }, new SubCategory()
            {
                Id = 10,
                Title = "خودرو",
                CategoryId = 3,

            },
            new SubCategory()
            {
                Id = 11,
                Title = "اسباب کشی",
                CategoryId = 4,

            },
            new SubCategory()
            {
                Id = 12,
                Title = "حمل بار",
                CategoryId = 4,

            },
            new SubCategory()
            {
                Id = 13,
                Title = "لوازم اشپزخانه",
                CategoryId = 5,

            }, new SubCategory()
            {
                Id = 14,
                Title = "لواز شستشو",
                CategoryId = 5,

            }, new SubCategory()
            {
                Id = 15,
                Title = "لوازم صوتی تصویری",
                CategoryId = 5,

            },
            new SubCategory()
            {
                Id = 16,
                Title = "لوازم اداری",
                CategoryId = 6,

            }, new SubCategory()
            {
                Id = 17,
                Title = "مبلمان اداری",
                CategoryId = 6,

            },
            new SubCategory()
            {
                Id = 18,
                Title = "نظافت",
                CategoryId = 7,

            },
            new SubCategory()
            {
                Id = 19,
                Title = "خشکشویی",
                CategoryId = 7,

            },
            new SubCategory()
            {
                Id = 20,
                Title = "قالیشویی",
                CategoryId = 7,

            },
            new SubCategory()
            {
                Id = 21,
                Title = "سم پاشی",
                CategoryId = 7,

            },
              new SubCategory()
              {
                  Id = 22,
                  Title = "موبایل و تبلت",
                  CategoryId = 8,

              }, new SubCategory()
              {
                  Id = 23,
                  Title = "خدمات کامپیوتری",
                  CategoryId = 8,

              }, new SubCategory()
              {
                  Id = 24,
                  Title = "امنیت و شبکه",
                  CategoryId = 8,

              }




            );
    }
}
