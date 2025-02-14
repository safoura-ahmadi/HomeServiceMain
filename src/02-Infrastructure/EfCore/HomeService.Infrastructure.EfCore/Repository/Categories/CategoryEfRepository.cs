using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;


namespace HomeService.Infrastructure.EfCore.Repository.Categories;

public class CategoryEfRepository(ApplicationDbContext dbContext) : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
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
                        Tilte = sc.Title
                    }
                    ).ToList()
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch
        {
            return [];
        }
        ;
    }
    //admin
    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Categories.FirstAsync(c => c.Id == id, cancellationToken);
            item.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;

        }
        catch
        {
            return false;
        }
    }
    public async Task<bool> Update(int id, string title, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Categories.FirstAsync(c => c.Id == id && c.IsActive, cancellationToken);

            item.Title = title;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }

    }
    public async Task<List<GetCategoryForAdminPageDto>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Categories.AsNoTracking()
                .Where(c => c.IsActive)
                .Select(c => new GetCategoryForAdminPageDto
                {
                    Id = c.Id,
                    Title = c.Title
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch
        {
            return [];
        }


    }
    public async Task<bool> Create(string title, string imagePath, CancellationToken cancellationToken)
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
            return true;

        }
        catch
        {
            return false;
        }
    }
}

