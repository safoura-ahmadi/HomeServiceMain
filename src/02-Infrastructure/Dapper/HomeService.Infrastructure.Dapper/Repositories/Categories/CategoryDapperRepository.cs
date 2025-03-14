using Dapper;
using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Configs;
using HomeService.Infrastructure.Dapper.Queries.Categories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.Dapper.Repositories.Categories;

public class CategoryDapperRepository(SiteSetting siteSetting, ILogger<CategoryDapperRepository> logger) : ICategoryDapperRepo
{
    private readonly string _connectionString = siteSetting.ConnectionString.SqlConnection;

  

    public async Task<List<GetCategoryForAdminPageDto>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);
            var item = await connection.QueryAsync<GetCategoryForAdminPageDto>(CategoryQueries.GetAll, cancellationToken);
            return item.ToList();
        }
        catch(Exception ex) 
        {
            logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CategoryDapperRepository", ex.Message);

            return [];
        }
    }

 
}
