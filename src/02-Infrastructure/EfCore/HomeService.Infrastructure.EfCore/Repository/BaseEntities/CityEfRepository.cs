using HomeService.Domain.Core.Contracts.Repository.BaseEntities;
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace HomeService.Infrastructure.EfCore.Repository.BaseEntities;

public class CityEfRepository(ApplicationDbContext dbContext, ILogger<CityEfRepository> logger) : ICityRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<CityEfRepository> _logger = logger;

    public async Task<List<City>> GetAll(CancellationToken cancellationToken)
    {
       
        try
        {


            var items = await _dbContext.Cities.AsNoTracking()
                .ToListAsync(cancellationToken);
            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CityEfRepository", ex.Message);
            return [];
        }

    }
}
