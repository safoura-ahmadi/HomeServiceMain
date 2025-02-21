using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.EfCore.Repository.Users;

public class ExpertEfRepository(ApplicationDbContext dbContext, ILogger<ExpertEfRepository> logger) : IExpertRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<ExpertEfRepository> _logger = logger;

    public async Task<Result> Create(int userId, string lName, CancellationToken cancellationToken)
    {
        try
        {
            var item = new Expert()
            {
                Lname = lName,
                UserId = userId,

            };
            await _dbContext.Experts.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("کارشناس جدید با موفقیت ایجاد شد");

        }
        catch(Exception ex) 
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "ExpertEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Experts.AsNoTracking()
                .Include(e => e.User)
                .Where(e => e.User!.Status != Domain.Core.Enums.Users.UserStatusEnum.Rejected)
                .CountAsync(cancellationToken);
            return item;
        }
        catch(Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "ExpertEfRepository", ex.Message);
            return 0;
        }
    }
}
