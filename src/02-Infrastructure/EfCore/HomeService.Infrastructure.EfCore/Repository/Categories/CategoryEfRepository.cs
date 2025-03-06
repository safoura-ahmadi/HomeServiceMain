using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace HomeService.Infrastructure.EfCore.Repository.Categories;

public class CategoryEfRepository(ApplicationDbContext dbContext, ILogger<CategoryEfRepository> logger) : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<CategoryEfRepository> _logger = logger;

    public async Task<List<GetCategoryForMainPageDto>> GetAllForMainPage(CancellationToken cancellationToken)
    {
        
        try
        {
            var item = await _dbContext.Categories.AsNoTracking()
                .Where(c => c.IsActive)
                .Select(c => new GetCategoryForMainPageDto
                {
                    Id = c.Id,
                    Tilte = c.Title,
                    ImagePath = c.ImagePath,
                    SubCategories = c.SubCategories.Select(sc => new GetSubCategoryDto
                    {
                        Id = sc.Id,
                        Title = sc.Title
                    }
                    ).ToList()
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CategoryEfRepository", ex.Message);
            return [];
        }
        ;
    }
    //admin
    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (item is null)
                return Result.Fail("دسته بندی با این مشخصات یافت نشد");
            item.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("دسته بندی با موفقیت حذف شد");

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CategoryEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<Result> Update(int id, string title, string imagePath, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("دسته بندی با این مشخصات وجود ندارد");
            item.Title = title;
            item.ImagePath = imagePath;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("مشخصات دسته بندی با موفقیت ویرایش شد");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CategoryEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }

    }
    public async Task<List<GetCategoryForAdminPageDto>> GetAll(CancellationToken cancellationToken)
    {
       
        try
        {
            var item = await _dbContext.Categories.AsNoTracking()
                      .Where(c => c.IsActive)
                      .Include(c => c.SubCategories)
                      .ThenInclude(c => c.SubServices)
                       .Select(c => new GetCategoryForAdminPageDto
                       {
                           Id = c.Id,
                           Title = c.Title,
                           ImagePath = c.ImagePath,
                           SubServiceCount = c.SubCategories
                                         .SelectMany(sc => sc.SubServices)
                                         .Count(ss => ss.IsActive)
                       }).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CategoryEfRepository", ex.Message);
            return [];
        }


    }
    public async Task<Result> Create(string title, string imagePath, CancellationToken cancellationToken)
    {
        try
        {
            var item = new Category()
            {
                Title = title,
                ImagePath = imagePath,
                IsActive = true
            };
            await _dbContext.Categories.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok(" دسته بندی جدید با موفقیت ایجاد شد");

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CategoryEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }

    public async Task<Result> Update(UpdateCategoryDto model, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == model.Id && c.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("کتگوری با این مشخصات وجود ندارد");
            item.Title = model.Title;
            item.ImagePath = model.ImagePath ?? item.ImagePath;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("کتگوری با موفقیت ویرایش شد");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CategoryEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }

    }

    public async Task<UpdateCategoryDto?> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Categories.AsNoTracking()
                .Where(c => c.Id == id && c.IsActive)
                .Select(c => new UpdateCategoryDto
                {
                    Id = c.Id,
                    ImagePath = c.ImagePath,
                    Title = c.Title
                }).FirstOrDefaultAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CategoryEfRepository", ex.Message);
            return null;
        }
    }
}

