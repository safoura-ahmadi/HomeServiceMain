using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Dtos.Categories;
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
                    Tilte = sc.Title
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
                .Select(sc => new GetSubCategoryDto
                {

                    Id = sc.Id,
                    Tilte = sc.Title
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch
        {
            return [];
        }
    }
    public async Task<bool> Create(string title, int CategoryId, CancellationToken cancellationToken)
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
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubCategories.FirstAsync(c => c.Id == id, cancellationToken);
            item.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }

    }
    public async Task<bool> Update(UpdateSubCategoryDto model, CancellationToken cancellationToken)
    {

        try
        {
            var item = await _dbContext.SubCategories.FirstAsync(sc => sc.Id == model.Id && sc.IsActive, cancellationToken);

            item.Title = model.Tilte;
            item.CategoryId = model.CategoryId;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }

    }

}