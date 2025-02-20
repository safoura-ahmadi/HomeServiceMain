using HomeService.Domain.Core.Contracts.Repository.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Enums.Orders;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Infrastructure.EfCore.Repository.Orders;

public class SuggestionEfRepository(ApplicationDbContext dbContext) : ISuggestionRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;


    public async Task<Result> Create(SuggestionDto suggestion, CancellationToken cancellationToken)
    {
        try
        {
            var item = new Suggestion()
            {
                Description = suggestion.Description,
                ExpertId = suggestion.ExpertId,
                OrderId = suggestion.OrderId,
                TimeToDone = suggestion.TimeToDone,
                Price = suggestion.Price,
                IsAccepted = false,
                IsActive = false
            };
            await _dbContext.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("پیشنهاد با موفقیت ایجاد شد");

        }
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<List<GetSuggestionForOrderDto>> GetByOrderId(int orderId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.AsNoTracking()
                .Include(s => s.Expert)
                .Where(s => s.OrderId == orderId && s.IsActive)
                .Select(s => new GetSuggestionForOrderDto
                {
                    Id = s.Id,
                    ExperLname = s.Expert!.Lname,
                    Price = s.Price,
                    CreateAt = s.CreateAt,
                    ExpertLname = s.Expert!.Lname ?? "نامشخص",
                    TimeToDone = s.TimeToDone,
                    IsAccepted = s.IsAccepted
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch
        {
            return [];
        }
    }
    public async Task<Result> ChangeStatetoAccepted(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.FirstAsync(s => s.Id == id && s.IsActive, cancellationToken);
            item.IsAccepted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("وضعیت پیشنهاد در حالت تایید شده قرار گرفت");
        }

        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<Result> IsOrderHaveActiveSuggestion(int orderId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.Suggestions.AsNoTracking()
                .AnyAsync(s => s.OrderId == orderId && s.IsActive, cancellationToken);
            if (result is true)
                return Result.Ok();
            else
                return Result.Fail("پیشنهاد فعالی برای این سفارش وجود ندارد");
        }
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<Result> IsOrderHaveAcceptedSugestion(int orderId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.Suggestions.AsNoTracking()
                .AnyAsync(s => s.OrderId == orderId && s.IsActive && s.IsAccepted, cancellationToken);
            if (result is true)
                return Result.Ok();
            else
                return Result.Fail("پیشنهاد تایید شده ای برای این سفارش وجود ندارد");
        }
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }

    //admin
    public async Task<List<SuggestionDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.AsNoTracking()
                .Where(s => s.IsActive)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(s => s.Expert)
                .Select(s => new SuggestionDto
                {
                    Id = s.Id,
                    Description = s.Description,
                    ExperLname = s.Expert!.Lname,
                    ExpertId = s.ExpertId,
                    OrderId = s.OrderId,
                    Price = s.Price,
                    TimeToDone = s.TimeToDone
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
            var item = await _dbContext.Suggestions.AsNoTracking()
                .CountAsync(cancellationToken);
            return item;
        }
        catch
        {
            return 0;
        }
    }
    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (item is null)
                return Result.Fail("سفارشی  با این مشخصات برای حذف کردن یافت نشد");
            item.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("سفارش با موفقیت حذف شد");
        }
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    //
    public async Task<List<GetlastSuggestionDto>> GetLatestSuggestions(CancellationToken cancellationToken)
    {
        try
        {
            var items = await _dbContext.Suggestions.AsNoTracking()
                .Include(s => s.Order)
                .ThenInclude(o => o!.SubService)
                .Include(s => s.Expert)
                .OrderByDescending(s => s.CreateAt)
                .Take(10)
                .Select(s => new GetlastSuggestionDto
                {
                    Id = s.Id,
                    CreateAt = s.CreateAt,
                    ExperLname = s.Expert!.Lname ?? "نامشخص",
                    Price = s.Price,
                    SubServiceName = s.Order!.SubService!.Title
                }
                ).ToListAsync(cancellationToken);
            return items;

        }
        catch
        {
            return [];
        }
    }
}
