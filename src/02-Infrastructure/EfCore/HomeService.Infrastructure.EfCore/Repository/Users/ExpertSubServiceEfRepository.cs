using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.EfCore.Repository.Users;

public class ExpertSubServiceEfRepository(ApplicationDbContext dbContext, ILogger<ExpertEfRepository> logger) : IExpertSubServiceRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<ExpertEfRepository> _logger = logger;

    public async Task<List<int>> GetSubServicesByExpertId(int expertId, CancellationToken cancellationToken)
    {
        try
        {
            var subServices = await _dbContext.ExpertSubServices
                .AsNoTracking()
                .Where(e => e.ExpertId == expertId && e.SubService!.IsActive)
                .Select(e => e.SubServiceId)
                .ToListAsync(cancellationToken);

            return subServices;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "ExpertSubServiceEfRepository", ex.Message);
            return [];
        }

    }
    public async Task<bool> Create(int expertId, List<int> subServiceIds, CancellationToken cancellationToken)
    {
        try
        {

            var items = subServiceIds.Select(subServiceId => new ExpertSubService
            {
                ExpertId = expertId,
                SubServiceId = subServiceId,
            }).ToList();


            await _dbContext.ExpertSubServices.AddRangeAsync(items, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "ExpertSubServiceEfRepository", ex.Message);
            return false;
        }
    }

    public async Task<bool> Delete(int expertId, CancellationToken cancellationToken)
    {
        try
        {
            var items = await _dbContext.ExpertSubServices
                .Where(e => e.ExpertId == expertId || e.SubService!.IsActive)
                .ToListAsync(cancellationToken);
            if (items == null || !items.Any())
            {
                return false;
            }

            _dbContext.ExpertSubServices.RemoveRange(items);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "ExpertSubServiceEfRepository", ex.Message);
            return false;
        }
    }
}
