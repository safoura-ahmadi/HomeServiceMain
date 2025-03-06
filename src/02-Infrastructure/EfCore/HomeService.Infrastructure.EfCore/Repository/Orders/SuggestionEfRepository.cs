using HomeService.Domain.Core.Contracts.Repository.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Domain.Core.Enums.Orders;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.EfCore.Repository.Orders;

public class SuggestionEfRepository(ApplicationDbContext dbContext, ILogger<SuggestionEfRepository> logger) : ISuggestionRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<SuggestionEfRepository> _logger = logger;

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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<List<GetSuggestionForOrderDto>> GetByOrderId(int orderId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.AsNoTracking()
                .Include(s => s.Expert)
                .ThenInclude(e => e!.User)
                .Where(s => s.OrderId == orderId && s.IsActive)
                .Select(s => new GetSuggestionForOrderDto
                {
                    Id = s.Id,
                    ExperLname = s.Expert!.User!.Lname??"نامشخص",
                    Price = s.Price,
                    CreateAt = s.CreateAt,
                    TimeToDone = s.TimeToDone,
                    IsAccepted = s.IsAccepted,
                    ExpertId = s.ExpertId
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);
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

        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);
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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);
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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);
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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);
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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);
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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);
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
                .ThenInclude(e => e.User)
                 .Where(o => o.Order!.IsActive)
                .OrderByDescending(s => s.CreateAt)
                .Take(10)
                .Select(s => new GetlastSuggestionDto
                {
                    Id = s.Id,
                    CreateAt = s.CreateAt,
                    ExperLname = s.Expert!.User!.Lname ?? "نامشخص",
                    Price = s.Price,
                    SubServiceName = s.Order!.SubService!.Title,
                    ExpertId = s.ExpertId
                }
                ).ToListAsync(cancellationToken);
            return items;

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);
            return [];
        }
    }

    public async Task<SuggestionDetailsDto?> GetDetailById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.AsNoTracking()
                .Where(s => s.Id == id && s.IsActive)
                .Include(s => s.Expert)
                .Select(s => new SuggestionDetailsDto
                {
                    Id = s.Id,
                    Description = s.Description,
                    ExpertLname = s.Expert!.User!.Lname ?? "نامشخص",
                    OrderId = s.OrderId,
                    Score = 0,
                    ExpertId = s.ExpertId

                }).FirstOrDefaultAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);

            return null;
        }
    }

    public async Task<int> GetExpertActiveSuggestionsCount(int expertId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.AsNoTracking()
                .Where(s => s.ExpertId == expertId && s.IsActive)
                .CountAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);

            return 0;
        }
    }

    public async Task<int> GetCustomerActiveSuggestionsCount(int customerId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.AsNoTracking()
                .Include(s => s.Order)
                .Where(s => s.Order!.CustomerId == customerId && s.IsActive)
                .CountAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);

            return 0;
        }
    }

    public async Task<SuggestionOverviewDto?> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Suggestions.AsNoTracking()
                .Where(s => s.Id == id && s.IsActive)
                .Select(s => new SuggestionOverviewDto
                {
                    Id = s.Id,
                    ExpertId = s.ExpertId,
                    OrderId = s.OrderId,
                    Price = s.Price,
                    TimeToDone = s.TimeToDone,
                }).FirstOrDefaultAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "SuggestionEfRepository", ex.Message);
            return null;
        }
    }
}
