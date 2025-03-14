using Dapper;
using HomeService.Domain.Core.Contracts.Repository.BaseEntities;
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Entities.Configs;
using HomeService.Infrastructure.Dapper.Queries.BaseEntities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.Dapper.Repositories.BaseEntites;

public class CityDapperRepository(SiteSetting siteSetting,ILogger<CityDapperRepository> logger) : ICityRepository
{
    private readonly string _connectionString = siteSetting.ConnectionString.SqlConnection;
    public async Task<List<City>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);
            var item = await connection.QueryAsync<City>(CityQueries.GetAll, cancellationToken);
            return item.ToList();
        }
        catch (Exception ex)
        {
            logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CityDapperRepository", ex.Message);

            return [];
        }
    }
}
