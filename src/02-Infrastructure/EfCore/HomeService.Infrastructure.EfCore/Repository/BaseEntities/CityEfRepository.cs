using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System;

namespace HomeService.Infrastructure.EfCore.Repository.BaseEntities;

public class CityEfRepository(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<List<City>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _dbContext.Cities.AsNoTracking()
            .ToListAsync(cancellationToken);
        return items;
    }
}
