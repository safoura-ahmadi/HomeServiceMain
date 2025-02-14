using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Infrastructure.EfCore.Repository.Users;

public class CustomerEfRepository(ApplicationDbContext dbContext) : ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<bool> IsConfirmedByAdmin(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.Customers
                            .AsNoTracking()
                            .Where(e => e.UserId == userId)
                            .Select(e => e.IsConfirmed)
                            .FirstAsync(cancellationToken);
            return result;
        }
        catch
        {
            return false;
        }
    }
    public async Task<GetUsertDto?> GetByUserId(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Customers.AsNoTracking()
                .Where(e => e.UserId == userId)
                .Select(e => new GetUsertDto
                {
                    Id = e.Id,
                    CityId = e.CityId,
                    Balance = e.Balance,
                    Fname = e.Fname,
                    ImagePath = e.ImagePath,
                    Lname = e.Lname,
                    UserId = userId,
                }
                ).FirstOrDefaultAsync(cancellationToken);
            return item;
        }
        catch
        {
            return null;
        }
    }
    public async Task<bool> Update(GetUsertDto model, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Customers.FirstAsync(e => e.Id == model.Id, cancellationToken);
            item.Fname = model.Fname;
            item.ImagePath = model.ImagePath;
            item.Lname = model.Lname;
            item.UserId = model.UserId;
            item.Biography = model.Biography;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }

    }
    //admin
    public async Task<bool> ConfirmById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Customers.FirstAsync(e => e.Id == id, cancellationToken);
            item.IsConfirmed = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }

    }
    public async Task<bool> UnConfirmById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Customers.FirstAsync(e => e.Id == id, cancellationToken);
            item.IsConfirmed = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }

    }

    public async Task<List<GetUsertDto>> GetAll(int pageNumber,int pageSize,CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Customers.AsNoTracking()
                .Skip((pageNumber * - 1)* pageSize)
                .Take(pageSize)
                .Select(e => new GetUsertDto
                {
                    Id = e.Id,
                    Balance = e.Balance,
                    Biography = e.Biography,
                    CityId = e.CityId,
                    Fname = e.Fname,
                    ImagePath = e.ImagePath,
                    Lname = e.Lname,
                    UserId = e.UserId,

                }).ToListAsync(cancellationToken);
            return item;
        }
        catch
        {
            return [];
        }

    }
    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Customers.AsNoTracking()
                .CountAsync(cancellationToken);
            return item;
        }
        catch
        {
            return 0;
        }
    }


    public async Task<bool> WithdrawBalance(int customerId, decimal money, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Customers
                .Where(c => c.Id == customerId)
                .FirstAsync(cancellationToken);
            if (item.Balance < money)
                return false;
            item.Balance -= money;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;


        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ChargeBalance(int customerId, decimal money, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Customers
                .Where(c => c.Id == customerId)
                .FirstAsync(cancellationToken);
            item.Balance += money;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;


        }
        catch
        {
            return false;
        }
    }
}
