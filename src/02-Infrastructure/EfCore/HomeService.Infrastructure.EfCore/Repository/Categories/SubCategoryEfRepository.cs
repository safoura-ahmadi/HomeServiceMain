using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Infrastructure.EfCore.Repository.Categories;

public class SubCategoryEfRepository(ApplicationDbContext dbContext) : ISubCategoryRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<GetSubCategoryDto>> GetByCategoryId(int categoryId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubCategories.AsNoTracking()
                .Where(sc => sc.IsActive && sc.CategoryId == categoryId)
                .Select(sc => new GetSubCategoryDto
                {

                    Id = sc.Id,
                    Title = sc.Title
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch
        {
            return [];
        }
    }
    //admin
    public async Task<List<GetSubCategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubCategories.AsNoTracking()
                .Where(sc => sc.IsActive)
                .Include(sc => sc.Category)
                .Select(sc => new GetSubCategoryDto
                {

                    Id = sc.Id,
                    Title = sc.Title,
                    CategoryName = sc.Category!.Title
                  
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch
        {
            return [];
        }
    }
    public async Task<Result> Create(string title, int CategoryId, CancellationToken cancellationToken)
    {
        try
        {
            var item = new SubCategory()
            {
                Title = title,
                CategoryId = CategoryId,
                IsActive = true
            };
            await _dbContext.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("سابکتگوری با موفقیت ایحاد شد");
        }
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubCategories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (item is null)
                return Result.Fail("سابکتگوری با این مشخصات وجود ندارد");
            item.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("سابکتگوری با موفقیت حذف شد");
        }
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }

    }
    public async Task<Result> Update(UpdateSubCategoryDto model, CancellationToken cancellationToken)
    {

        try
        {
            var item = await _dbContext.SubCategories.FirstOrDefaultAsync(sc => sc.Id == model.Id && sc.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("سابکتگوری با این مشخصات وجود ندارد");
            item.Title = model.Title;
            item.CategoryId = model.CategoryId;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("سلبکتگوری با موفقیت ویرایش شد");
        }
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }

    }

    public async Task<UpdateSubCategoryDto?> GetById(int id, CancellationToken cancellationToken)
    {
        var item = await _dbContext.SubCategories.AsNoTracking()
            .Where(sc => sc.Id == id && sc.IsActive)
            .Select(sc => new UpdateSubCategoryDto
            {
                Id = sc.Id,
                Title = sc.Title,
                CategoryId = sc.CategoryId
            }).FirstOrDefaultAsync(cancellationToken);
        return item;
    }
}