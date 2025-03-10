using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.EfCore.Repository.Users;

public class CustomerEfRepository(ApplicationDbContext dbContext, ILogger<CustomerEfRepository> logger) : ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<CustomerEfRepository> _logger = logger;

    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Customers.AsNoTracking()
                .Include(c => c.User)
                .Where(c => c.User!.Status != Domain.Core.Enums.Users.UserStatusEnum.Rejected)
                .CountAsync(cancellationToken);
            return item;
        }
        catch(Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CustomerEfRepository", ex.Message);
            return 0;
        }
    }

    public async Task<Result> Create(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var item = new Customer()
            {
              
                UserId = userId,

            };
            await _dbContext.Customers.AddAsync(item,cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("مشتری جدید با موفقیت ایجاد شد");

        }
        catch(Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CustomerEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }

}
