using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Infrastructure.EfCore.Repository.Categories;

public class SubServiceEfRepository(ApplicationDbContext dbContext) : ISubServiceRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<GetSubServiceDto>> GetBySubCategoryId(int subcategoryId, CancellationToken cancellationToken)
    {
        var item = await _dbContext.SubServices.AsNoTracking()
             .Where(s => s.IsActive && s.SubCategoryId == subcategoryId)
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
        catch
        {
            return 0;
        }
    }

    //admin
    public async Task<bool> Update(GetSubServiceDto model, CancellationToken cancellationToken)

    {
        try
        {
            var item = await _dbContext.SubServices.FirstAsync(s => s.Id == model.Id, cancellationToken);
            item.Title = model.Title;
            item.Description = model.Description;
            item.ImagePath = model.ImagePath;
            item.BasePrice = model.BasePrice;
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
            var item = await _dbContext.SubServices.FirstAsync(s => s.Id == id, cancellationToken);
            item.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<List<GetSubServiceDto>> GetAll(int pageNumber,int pageSize,CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubServices.AsNoTracking()
                 .Where(s => s.IsActive)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
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
        catch
        {
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
        catch
        {
            return 0;
        }
    }
}
