using HomeService.Domain.Core.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.Categories;

public class SubServiceConfiguration : IEntityTypeConfiguration<SubService>
{
    public void Configure(EntityTypeBuilder<SubService> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(55)
            .HasColumnType("nvarchar");

        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("nvarchar");

        builder.HasOne(S => S.SubCategory)
            .WithMany(sc => sc.SubServices)
            .HasForeignKey(s => s.SubCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
            new SubService()
            {
                Id = 1,
                Title = "کاشی و سرامیک",
                Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                SubCategoryId = 1,
                IsActive = true,
                BasePrice = 300000,
                ImagePath = "\\UserTemplate\\images\\icon\\banaii.png"


            },
                new SubService()
                {
                    Id = 2,
                    Title = "گچ کاری",
                    Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                    SubCategoryId = 1,
                    IsActive = true,
                    BasePrice = 200000,
                    ImagePath = "\\UserTemplate\\images\\icon\\gachKari.png"

                },
                    new SubService()
                    {
                        Id = 3,
                        Title = "شیشه بری",
                        Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                        SubCategoryId = 4,
                        IsActive = true,
                        BasePrice = 300000,
                        ImagePath = "\\UserTemplate\\images\\icon\\shisheBori.png"

                    },
                        new SubService()
                        {
                            Id = 4,
                            Title = "کولر ابی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 7,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\waterCooler.png"


                        }, new SubService()
                        {
                            Id = 5,
                            Title = "کولر گازی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 7,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\GasCooler.png"

                        }, new SubService()
                        {
                            Id = 6,
                            Title = "لوله بازکنی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 8,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\openPipe.png"


                        }, new SubService()
                        {
                            Id = 7,
                            Title = "برقکاری ساختمان",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 9,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\powerWork.png"


                        }, new SubService()
                        {
                            Id = 8,
                            Title = "روغنکاری ماشین",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 10,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\oilChanging.png"


                        }, new SubService()
                        {
                            Id = 9,
                            Title = "نقاشی و صافکاری",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 10,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\carRepairing.png"


                        }
                        , new SubService()
                        {
                            Id = 10,
                            Title = "اسباب کشی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 11,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\changingPlace.png"


                        }
                        , new SubService()
                        {
                            Id = 11,
                            Title = "حمل بار جزعی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 12,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\partialChanging.png"


                        }
                        , new SubService()
                        {
                            Id = 12,
                            Title = "یخچال و فریزر",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 13,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\freezer.png"


                        }
                        , new SubService()
                        {
                            Id = 13,
                            Title = "ماشین لباس شویی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 14,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\washingMachine.png"


                        }, new SubService()
                        {
                            Id = 14,
                            Title = "فتوکپی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 15,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\photoCopy.png"


                        }, new SubService()
                        {
                            Id = 15,
                            Title = "لپ تاپ و نوت بوک",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 22,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\lapTop.png"


                        }, new SubService()
                        {
                            Id = 16,
                            Title = "موبایل و تبلت",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 22,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\mobileAndTablet.png"


                        }, new SubService()
                        {
                            Id = 17,
                            Title = "ارتقای سخت افزاری",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 23,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\hardWareUpgrade.png"


                        }, new SubService()
                        {
                            Id = 18,
                            Title = "شبکه کامپیوتری",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 24,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "\\UserTemplate\\images\\icon\\computerWebs.png"


                        }

            );
    }
}
