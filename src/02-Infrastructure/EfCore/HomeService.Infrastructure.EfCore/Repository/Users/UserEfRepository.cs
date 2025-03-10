using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Domain.Core.Enums.Users;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace HomeService.Infrastructure.EfCore.Repository.Users;

public class UserEfRepository(ApplicationDbContext dbContext, ILogger<UserEfRepository> logger) : IUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<UserEfRepository> _logger = logger;

    public async Task<List<GetAllUserDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Users.AsNoTracking()
                .Where(u => u.Status != UserStatusEnum.Rejected)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(u => u.City)
                .Select(u => new GetAllUserDto
                {
                    Id = u.Id,
                    Status = u.Status,
                    ImagePath = u.ImagePath,
                    Email = u.Email,
                    Lname = u.Lname ?? "نامشخص",
                    RoleName = _dbContext.Roles
                    .Where(r => _dbContext.UserRoles
                     .Any(ur => ur.UserId == u.Id && ur.RoleId == r.Id))
                    .Select(r => r.Name)
                    .FirstOrDefault()


                }).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "UserEfRepository", ex.Message);
            return [];
        }

    }
    public async Task<UserStatusEnum> IsConfirmedByAdmin(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.Users
                            .AsNoTracking()
                            .Where(u => u.Id == id)
                            .Select(u => u.Status)
                            .FirstAsync(cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "UserEfRepository", ex.Message);
            return UserStatusEnum.Pending;
        }
    }
    public async Task<Result> WithdrawBalance(int id, decimal money, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Users
                .Where(u => u.Id == id)
                .FirstAsync(cancellationToken);
            if (item.Balance < money)
                return Result.Fail("کیف پول شما مبلغ لازم برای پرداخت را ندارد. لطفا از قسمت پروفایل کیف پول خود را شارژ کنید");
            item.Balance -= money;

            return Result.Ok("مبلغ تعیین شده با موفقیت از حساب شما برداشت شد");


        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "UserEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<Result> ChargeBalance(int id, decimal money, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            if (item is null)
                return Result.Fail("کیف پولی یافت نشد");
            item.Balance += money;
        
            return Result.Ok("کیف پول با موفقیت شارژ شد");


        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "UserEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<Result> Update(UpdateUsertDto model, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == model.Id, cancellationToken);
            if (item is null)
                return Result.Fail("کاربری با این مشخصات یافت نشد");
            item.Fname = model.Fname;
            item.ImagePath = model.ImagePath;
            item.Lname = model.Lname;
            item.CityId = model.CityId;
            item.Address = model.Address;
            item.Balance = model.Balance;


            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("پروفایل کاربر با موفقیت اپدیت شد");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "UserEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }

    }
    public async Task<Result> ConfirmById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (item is null)
                return Result.Fail("کاربری با این مشخصات یافت نشد");
            item.Status = UserStatusEnum.Accepted;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("کاربر تایید شد");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "UserEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }

    }
    public async Task<Result> UnConfirmById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (item is null)
                return Result.Fail("کاربری با این مشخصات یافت نشد");
            item.Status = UserStatusEnum.Rejected;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("کاربر غیرفعال شد");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "UserEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }

    }

    public async Task<UpdateUsertDto?> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Users.AsNoTracking()
           .Where(u => u.Id == id)
           .Select(u => new UpdateUsertDto
           {
               Id = u.Id,
               Address = u.Address,
               Balance = u.Balance,
               CityId = u.CityId,
               Email = u.Email,
               Fname = u.Fname,
               Lname = u.Lname,
               ImagePath = u.ImagePath,
               Mobile = u.Mobile

           }).FirstOrDefaultAsync(cancellationToken);
            return item;


        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "UserEfRepository", ex.Message);
            return null;
        }
    }



    public async Task<GetUserStaticDataDto?> GetUserStaticDate(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Users.AsNoTracking()
                .Where(u => u.Id == id && u.Status != UserStatusEnum.Rejected)
                .Select(u => new GetUserStaticDataDto
                {

                    Fname = u.Fname,
                    ImagePath = u.ImagePath,

                }).FirstOrDefaultAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "UserEfRepository", ex.Message);
            return null;
        }
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetCityId(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Users.AsNoTracking()
                .Where(u => u.Id == id)
                .Select(u => u.CityId)
                .FirstOrDefaultAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "UserEfRepository", ex.Message);
            return 0;
        }
    }
}
