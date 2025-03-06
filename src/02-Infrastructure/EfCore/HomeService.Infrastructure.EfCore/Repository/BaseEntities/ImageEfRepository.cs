using HomeService.Domain.Core.Contracts.Repository.BaseEntities;
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.EfCore.Repository.BaseEntities;

public class ImageEfRepository(ApplicationDbContext dbContext, ILogger<CommentEfRepository> logger) : IImageRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<CommentEfRepository> _logger = logger;

    public async Task<bool> Create(List<string> imgAddress, int orderId, CancellationToken cancellationToken)
    {
        try
        {
            var images = imgAddress.Select(x => new Image()
            {
                Path = x,
                OrderId = orderId,
            });

            await _dbContext.Images.AddRangeAsync(images, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
                        _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "ImageEfRepository", ex.Message);

            return false;
        }
    }
}
