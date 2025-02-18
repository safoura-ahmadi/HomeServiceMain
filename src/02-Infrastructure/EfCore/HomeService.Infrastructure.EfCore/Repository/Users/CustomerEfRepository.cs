using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Infrastructure.EfCore.Repository.Users;

public class CustomerEfRepository(ApplicationDbContext dbContext) : ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Customers.AsNoTracking()
                .Include(c => c.User)
                .Where(c => c.User!.IsConfirmed)
                .CountAsync(cancellationToken);
            return item;
        }
        catch
        {
            return 0;
        }
    }

    public async Task<Result> Create(int userId,string lName, int cityId, CancellationToken cancellationToken)
    {
        try
        {
            var item = new Customer()
            {
                Lname = lName,
                CityId = cityId,
                UserId = userId,

            };
            await _dbContext.Customers.AddAsync(item,cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("مشتری جدید با موفقیت ایجاد شد");

        }
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }

}
