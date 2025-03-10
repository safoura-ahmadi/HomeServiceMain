using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Infrastructure.EfCore.Repository.Categories;

public class SubServiceEfRepository(ApplicationDbContext dbContext, ILogger<SubServiceEfRepository> logger) : ISubServiceRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<SubServiceEfRepository> _logger = logger;

    public async Task<Result> Create(CreateSubServiceDto model, CancellationToken cancellationToken)
    {
        try
        {
            var item = new SubService()
            {
                BasePrice = model.BasePrice,
                Description = model.Description,
                ImagePath = model.ImagePath,
                Title = model.Title,
                SubCategoryId = model.SubCategoryId,
                IsActive = true
            };
            await _dbContext.SubServices.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("سرویس جدید با موفقیت ایجاد شد");

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubServiceEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<List<GetSubServiceDto>> GetBySubCategoryId(int subcategoryId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubServices.AsNoTracking()
                .Include(s => s.SubCategory)
                .ThenInclude(sc => sc!.Category)
                 .Where(s => s.IsActive && s.SubCategoryId == subcategoryId)
                 .Select(s => new GetSubServiceDto
                 {
                     Id = s.Id,
                     Title = s.Title,
                     Description = s.Description,
                     BasePrice = s.BasePrice,
                     ImagePath = s.ImagePath,
                     SubCategoryTitle = s.SubCategory!.Title,
                     CategoryTitle = s.SubCategory!.Category!.Title
                 }
                 ).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubServiceEfRepository", ex.Message);
            return [];
        }
    }
    public async Task<List<GetSubServiceDto>> Search(string text, CancellationToken cancellationToken
        )
    {
        try
        {
            var item = await _dbContext.SubServices.AsNoTracking()
                 .Include(s => s.SubCategory)
                 .ThenInclude(c => c!.Category)
                 .Where(s => s.IsActive && s.Title.Contains(text) || s.Description.Contains(text) ||
                  s.SubCategory!.Title.Contains(text))
                               .Select(s => new GetSubServiceDto
                               {
                                   Id = s.Id,
                                   Title = s.Title,
                                   BasePrice = s.BasePrice,
                                   Description = s.Description,
                                   ImagePath = s.ImagePath,
                                   SubCategoryTitle = s.SubCategory!.Title,
                                   CategoryTitle = s.SubCategory!.Category!.Title

                               }).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubServiceEfRepository", ex.Message);
            return [];
        }
    }
    public async Task<int> GetBasePrice(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubServices.AsNoTracking()
                .Where(s => s.Id == id && s.IsActive)
                .Select(s => s.BasePrice)
                .FirstAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubServiceEfRepository", ex.Message);
            return 0;
        }
    }

    //admin
    public async Task<Result> Update(UpdateSubServiceDto model, CancellationToken cancellationToken)

    {
        try
        {
            var item = await _dbContext.SubServices.FirstOrDefaultAsync(s => s.Id == model.Id && s.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("هوم سرویسی با این مشخصات وجود ندارد");
            item.Title = model.Title;
            item.Description = model.Description;
            item.ImagePath = model.ImagePath ?? item.ImagePath;
            item.BasePrice = model.BasePrice;
            item.SubCategoryId = model.SubCategoryId;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("ویرایش هوم سرویس با موفقیت انجام شد");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubServiceEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubServices.FirstOrDefaultAsync(s => s.Id == id && s.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("هوم سرویسی با این مشخصات وجود ندارد");
            item.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("هوم سرویس با موفقیت حذف شد ");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubServiceEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<List<GetSubServiceDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubServices.AsNoTracking()
                 .Where(s => s.IsActive)
                 .Include(S => S.SubCategory)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .Select(s => new GetSubServiceDto
                 {
                     Id = s.Id,
                     Title = s.Title,
                     Description = s.Description,
                     BasePrice = s.BasePrice,
                     ImagePath = s.ImagePath,
                     SubCategoryTitle = s.SubCategory!.Title
                 }
                 ).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubServiceEfRepository", ex.Message);
            return [];
        }
    }
    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubServices.AsNoTracking()
                .CountAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubServiceEfRepository", ex.Message);
            return 0;
        }
    }

    public async Task<UpdateSubServiceDto?> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubServices.AsNoTracking()
                .Where(s => s.Id == id && s.IsActive)
                .Select(s => new UpdateSubServiceDto
                {
                    Id = s.Id,
                    BasePrice = s.BasePrice,
                    Description = s.Description,
                    SubCategoryId = s.SubCategoryId,
                    Title = s.Title
                }).FirstOrDefaultAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubServiceEfRepository", ex.Message);
            return null;
        }
    }

    public async Task<List<GetExpertPageSubServiceDto>> GetAllForExpertPages(CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubServices.AsNoTracking()
                .Where(s => s.IsActive)
                .Select(s => new GetExpertPageSubServiceDto
                {
                    Id = s.Id,
                    Title = s.Title
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubServiceEfRepository", ex.Message);
            return [];
        }

    }
}
