using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Infrastructure.EfCore.Repository.Orders;

public class SuggestionEfConfiguration(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<int> Count(CancellationToken cancellationToken)
    {

        var item = await _dbContext.Suggestions.AsNoTracking().CountAsync(cancellationToken);

        return item;
    }
    //public async Task<bool> Create(SuggestionDto suggestion, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        var item = new Suggestion()
    //        {
    //            Description = suggestion.Description,
    //            ExpertId = suggestion.ExpertId,
    //            OrderId = suggestion.OrderId,
    //            TimeToDone = suggestion.TimeToDone,
    //            Price = suggestion.Price,
    //            IsAccepted = false
    //        };
    //        await _dbContext.AddAsync(item);

    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}
}
