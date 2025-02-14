using HomeService.Domain.Core.Contracts.Repository.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Infrastructure.EfCore.Repository.Orders;

public class SuggestionEfConfiguration(ApplicationDbContext dbContext) : ISuggestionRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;


    public async Task<bool> Create(SuggestionDto suggestion, CancellationToken cancellationToken)
    {
        try
        {
            var item = new Suggestion()
            {
                Description = suggestion.Description,
                ExpertId = suggestion.ExpertId,
                OrderId = suggestion.OrderId,
                TimeToDone = suggestion.TimeToDone,
                Price = suggestion.Price,
                IsAccepted = false,
                IsActive = false
            };
            await _dbContext.AddAsync(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;

        }
        catch
        {
            return false;
        }
    }
    public async Task<List<SuggestionDto>> GetSuggestionByOrder(int orderId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.AsNoTracking()
                .Where(s => s.OrderId == orderId && s.IsActive)
                .Select(s => new SuggestionDto
                {
                    Id = s.Id,
                    Description = s.Description,
                    ExperLname = s.Expert.Lname,
                    Price = s.Price,
                    ExpertId = s.ExpertId,
                    OrderId = s.OrderId,
                    TimeToDone = s.TimeToDone
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch
        {
            return [];
        }
    }
    public async Task<bool> ChangeStatetoAccepted(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.FirstAsync(s => s.Id == id && s.IsActive, cancellationToken);
            item.IsAccepted = true;
            return true;
        }

        catch
        {
            return false;
        }
    }

    //admin
    public async Task<List<SuggestionDto>> GetAll(int pageNumber,int pageSize,CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.AsNoTracking()
                .Where(s => s.IsActive)
                .Skip((pageNumber  - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SuggestionDto
                {
                    Id = s.Id,
                    Description = s.Description,
                    ExperLname = s.Expert.Lname,
                    ExpertId = s.ExpertId,
                    OrderId = s.OrderId,
                    Price = s.Price,
                    TimeToDone = s.TimeToDone
                }).ToListAsync(cancellationToken);
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
            var item = await _dbContext.Suggestions.AsNoTracking()
                .CountAsync(cancellationToken);
            return item;
        }
        catch
        {
            return 0;
        }
    }
    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.FirstAsync(s => s.Id == id);
            item.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
   
}
