using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.EfCore.Repository.Categories;

public class SubCategoryEfRepository(ApplicationDbContext dbContext, ILogger<SubCategoryEfRepository> logger) : ISubCategoryRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<SubCategoryEfRepository> _logger = logger;

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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubCategoryEfRepository", ex.Message);
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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubCategoryEfRepository", ex.Message);
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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubCategoryEfRepository", ex.Message);
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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubCategoryEfRepository", ex.Message);
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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubCategoryEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }

    }

    public async Task<UpdateSubCategoryDto?> GetById(int id, CancellationToken cancellationToken)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubCategoryEfRepository", ex.Message);
            return null;
        }
    }

    public async Task<List<GetAllSubCategoryWithServiceDto>> GetAllWithService(CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubCategories.AsNoTracking()
                .Where(sc => sc.IsActive)
                .Select(sc => new GetAllSubCategoryWithServiceDto
                {
                    SubCategory = new GetSubCategoryDto
                    {
                        Id = sc.Id,
                        CategoryName = sc.Category!.Title,
                        Title = sc.Title

                    },
                    SubService = sc.SubServices
                    .Where(s => s.IsActive)
                    .Select(s => new GetSubCategoryServiceDto
                    {
                        Id = s.Id,
                        BasePrice = s.BasePrice,
                        Description = s.Description,
                        ImagePath = s.ImagePath,
                        Title = s.Title
                    }).ToList()


                }).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubCategoryEfRepository", ex.Message);
            return [];
        }
    }
}