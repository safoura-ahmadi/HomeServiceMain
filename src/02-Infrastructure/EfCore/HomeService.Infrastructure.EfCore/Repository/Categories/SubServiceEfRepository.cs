using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Infrastructure.EfCore.Repository.Categories;

public class SubServiceEfRepository(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<List<GetSubServiceDto>> GetAll(CancellationToken cancellationToken)
    {
        var item = await _dbContext.SubServices.AsNoTracking()
             .Where(s => s.IsActive)
             .Select(s => new GetSubServiceDto
             {
                 Id = s.Id,
                 Title = s.Title,
                 Description = s.Description,
                 BasePrice = s.BasePrice,
                 ImagePath = s.ImagePath,

             }
             ).ToListAsync(cancellationToken);
        return item;
    }
    public async Task<List<GetSubServiceDto>> GetBySubCategoryId(int categoryId, CancellationToken cancellationToken)
    {
        var item = await _dbContext.SubServices.AsNoTracking()
             .Where(s => s.IsActive && s.SubCategoryId == categoryId)
             .Select(s => new GetSubServiceDto
             {
                 Id = s.Id,
                 Title = s.Title,
                 Description = s.Description,
                 BasePrice = s.BasePrice,
                 ImagePath = s.ImagePath,

             }
             ).ToListAsync(cancellationToken);
        return item;
    }
    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var item = await _dbContext.SubServices.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (item is null)
            return false;
        item.IsActive = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<List<GetSubServiceDto>> Search(string text, CancellationToken cancellationToken
        )
    {
        var item = await _dbContext.SubServices.AsNoTracking()
            .Where(s => s.IsActive && s.Title.Contains(text) || s.Description.Contains(text))
            .Select(s => new GetSubServiceDto
            {
                Id = s.Id,
                Title = s.Title,
                BasePrice = s.BasePrice,
                Description = s.Description,
                ImagePath = s.ImagePath


            }).ToListAsync(cancellationToken);
        return item;
    }
    public async Task<bool> Update(GetSubServiceDto model, CancellationToken cancellationToken)
    {
        var item = await _dbContext.SubServices.FirstOrDefaultAsync(s => s.Id == model.Id, cancellationToken);
        if (item is null)
            return false;
        item.Title = model.Title;
        item.Description = model.Description;
        item.ImagePath = model.ImagePath;
        item.BasePrice = model.BasePrice;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<int> GetBasePrice(int id, CancellationToken cancellationToken )
    {
        var item = await _dbContext.SubServices.AsNoTracking()
            .Where(s => s.Id == id && s.IsActive)
            .FirstOrDefaultAsync(cancellationToken);
        if (item is null)
            return 0;
        return item.BasePrice;
    }

}
