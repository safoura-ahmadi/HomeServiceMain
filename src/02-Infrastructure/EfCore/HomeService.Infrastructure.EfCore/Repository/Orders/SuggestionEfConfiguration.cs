using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HomeService.Infrastructure.EfCore.Repository.Orders;

public class SuggestionEfConfiguration(ApplicationDbContext dbContext)
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
    public async Task<bool> ChangeStatetoAccepted(int id, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Suggestions.FirstOrDefaultAsync(s => s.Id == id && s.IsActive, cancellationToken);
        if (item is null)
            return false;
        item.IsAccepted = true;
        return true;
    }

    //admin
    public async Task<List<SuggestionDto>> GetAll(CancellationToken cancellationToken)
    {
        var item = await _dbContext.Suggestions.AsNoTracking()
            .Where(s => s.IsActive)
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
    public async Task<bool> Delete(int id,CancellationToken cancellationToken)
    {
        var item = await _dbContext.Suggestions.FirstOrDefaultAsync(s => s.Id == id);
        if (item is null)
            return false;
        item.IsActive = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<int> Count(CancellationToken cancellationToken)
    {

        var item = await _dbContext.Suggestions.AsNoTracking().CountAsync(cancellationToken);

        return item;
    }
}
