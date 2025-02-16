using HomeService.Domain.Core.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.Categories;

public class SubServiceConfiguration : IEntityTypeConfiguration<SubService>
{
    public void Configure(EntityTypeBuilder<SubService> builder)
    {
        //\\UserTemplate\\images\\icon\\banaii.png

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
                ImagePath = "banaii.png"


            },
                new SubService()
                {
                    Id = 2,
                    Title = "گچ کاری",
                    Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                    SubCategoryId = 1,
                    IsActive = true,
                    BasePrice = 200000,
                    ImagePath = "gachKari.png"

                },
                    new SubService()
                    {
                        Id = 3,
                        Title = "شیشه بری",
                        Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                        SubCategoryId = 4,
                        IsActive = true,
                        BasePrice = 300000,
                        ImagePath = "shisheBori.png"

                    },
                        new SubService()
                        {
                            Id = 4,
                            Title = "کولر ابی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 7,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "waterCooler.png"


                        }, new SubService()
                        {
                            Id = 5,
                            Title = "کولر گازی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 7,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "GasCooler.png"

                        }, new SubService()
                        {
                            Id = 6,
                            Title = "لوله بازکنی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 8,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "openPipe.png"


                        }, new SubService()
                        {
                            Id = 7,
                            Title = "برقکاری ساختمان",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 9,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "powerWork.png"


                        }, new SubService()
                        {
                            Id = 8,
                            Title = "روغنکاری ماشین",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 10,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "oilChanging.png"


                        }, new SubService()
                        {
                            Id = 9,
                            Title = "نقاشی و صافکاری",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 10,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "carRepairing.png"


                        }
                        , new SubService()
                        {
                            Id = 10,
                            Title = "اسباب کشی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 11,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "changingPlace.png"


                        }
                        , new SubService()
                        {
                            Id = 11,
                            Title = "حمل بار جزعی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 12,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "partialChanging.png"


                        }
                        , new SubService()
                        {
                            Id = 12,
                            Title = "یخچال و فریزر",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 13,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "freezer.png"


                        }
                        , new SubService()
                        {
                            Id = 13,
                            Title = "ماشین لباس شویی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 14,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "washingMachine.png"


                        }, new SubService()
                        {
                            Id = 14,
                            Title = "فتوکپی",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 15,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "photoCopy.png"


                        }, new SubService()
                        {
                            Id = 15,
                            Title = "لپ تاپ و نوت بوک",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 22,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "lapTop.png"


                        }, new SubService()
                        {
                            Id = 16,
                            Title = "موبایل و تبلت",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 22,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "mobileAndTablet.png"


                        }, new SubService()
                        {
                            Id = 17,
                            Title = "ارتقای سخت افزاری",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 23,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "hardWareUpgrade.png"


                        }, new SubService()
                        {
                            Id = 18,
                            Title = "شبکه کامپیوتری",
                            Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است",
                            SubCategoryId = 24,
                            IsActive = true,
                            BasePrice = 300000,
                            ImagePath = "computerWebs.png"


                        }

            );
    }
}
