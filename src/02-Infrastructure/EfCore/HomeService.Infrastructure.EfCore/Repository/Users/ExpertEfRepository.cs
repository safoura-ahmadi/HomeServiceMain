using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Infrastructure.EfCore.Repository.Users;

public class ExpertEfRepository(ApplicationDbContext dbContext) : IExpertRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
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
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Experts.AsNoTracking()
                .Include(e => e.User)
                .Where(e => e.User!.IsConfirmed)
                .CountAsync(cancellationToken);
            return item;
        }
        catch
        {
            return 0;
        }
    }
}
