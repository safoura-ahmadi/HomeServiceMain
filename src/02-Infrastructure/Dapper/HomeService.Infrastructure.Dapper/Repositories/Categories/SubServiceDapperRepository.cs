using Dapper;
using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Configs;
using HomeService.Infrastructure.Dapper.Queries.Categories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.Dapper.Repositories.Categories;

public class SubServiceDapperRepository(SiteSetting siteSetting, ILogger<SubServiceDapperRepository> logger) : ISubServiceDapperRepo
{
    private readonly string _connectionString = siteSetting.ConnectionString.SqlConnection;
   
 

    public async Task<List<GetSubServiceDto>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);
            var item = await connection.QueryAsync<GetSubServiceDto>(SubServiceQueries.GetAll, cancellationToken);
            return item.ToList();
        }
        catch (Exception ex)
        {
            logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SubServiceDapperRepository", ex.Message);

            return [];
        }
    }

   
}
