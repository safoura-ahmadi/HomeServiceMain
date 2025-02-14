using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Infrastructure.EfCore.Repository.Users;

public class ExpertSubServiceEfRepository(ApplicationDbContext dbContext) : IExpertSubServiceRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<Dictionary<int, string>> GetSubServicesForExpert(int expertId, CancellationToken cancellationToken)
    {
        try
        {
            var subServices = await _dbContext.ExpertSubServices
                .AsNoTracking()
                .Where(e => e.ExpertId == expertId)
                .Select(e => new { e.SubService.Id, e.SubService.Title })
                .ToDictionaryAsync(e => e.Id, e => e.Title, cancellationToken);

            return subServices;
        }
        catch
        {
            return [];
        }
        
    }
    public async Task<bool> Create(int expertId, int subServiceId, CancellationToken cancellationToken)
    {
        try
        {
            var item = new ExpertSubService()
            {
                ExpertId = expertId,
                SubServiceId = subServiceId,
            };
            await _dbContext.ExpertSubServices.AddAsync(item,cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
