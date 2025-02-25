using HomeService.Domain.Core.Contracts.Repository.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Enums.Orders;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace HomeService.Infrastructure.EfCore.Repository.Orders;

public class OrderEfRepository(ApplicationDbContext dbContext, ILogger<OrderEfRepository> logger) : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<OrderEfRepository> _logger = logger;

    public async Task<int> Create(CreateOrderDto order, CancellationToken cancellationToken)
    {
        try
        {
            var item = new Order
            {
                TimeToDone = order.TimeToDone,
                CustomerId = order.CustomerId,
                CreateAt = DateTime.Now,
                Description = order.Description,
                Price = order.Price,
                IsActive = true,
                Status = Domain.Core.Enums.Orders.OrderStatusEnum.WaitingForExpertOffer,
                SubServiceId = order.SubServiceId,
            };
            await _dbContext.Orders.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return item.Id;

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return 0;
        }
    }
    public async Task<List<GetOrderDto>> GetAvailableOrdersForExpert(int cityId, int subserviceId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
                .Where(o => o.IsActive && o.Customer!.CityId == cityId &&
                o.SubServiceId == subserviceId &&
                (o.Status == OrderStatusEnum.WaitingForExpertOffer ||
                o.Status == OrderStatusEnum.WaitingForExpertSelection))
                .Select(o => new GetOrderDto
                {
                    Id = o.Id,
                    CreateAt = o.CreateAt,
                    CustomerId = o.CustomerId,
                    CustomerLname = o.Customer!.Lname,
                    Description = o.Description,
                    Price = o.Price,
                    TimeToDone = o.TimeToDone
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return [];
        }


    }
    public async Task<Result> ChangeStateToWaitingForExpertOffer(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id && o.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("سفارشی با این مشخص یافت نشد");
            item.Status = OrderStatusEnum.WaitingForExpertOffer;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("وضعیت سفارش با موفقیت تغییر یافت");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<Result> ChangeStateToWaitingForExpertSelection(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id && o.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("سفارشی با این مشخص یافت نشد");
            item.Status = OrderStatusEnum.WaitingForExpertSelection;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("وضعیت سفارش با موفقیت تغییر یافت");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<Result> ChangeStateToWorkCompletedAndPaid(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id && o.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("سفارشی با این مشخص یافت نشد");
            item.Status = OrderStatusEnum.WorkCompletedAndPaid;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("وضعیت سفارش با موفقیت تغییر یافت");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<Result> ChangeStateToExpertArrivedAtLocation(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id && o.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("سفارشی با این مشخص یافت نشد");
            item.Status = OrderStatusEnum.ExpertArrivedAtLocation;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("وضعیت سفارش با موفقیت تغییر یافت");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<OrderStatusEnum> GetLastStatusOfOrder(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id && o.IsActive, cancellationToken);
            if (item is null)
                return OrderStatusEnum.UnDefined;
            return item.Status;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return OrderStatusEnum.UnDefined;
        }
    }
    //admin
    public async Task<List<GetAllOrderDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
                .Where(o => o.IsActive)
                 .Include(o => o.Customer)
                 .Include(o => o.SubService)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new GetAllOrderDto
                {
                    Id = o.Id,
                    CreateAt = o.CreateAt,
                    Status = o.Status,
                    SubServiceName = o.SubService!.Title,
                    CustomerLname = o.Customer!.Lname ?? "نامشخص",
                    TimeToDone = o.TimeToDone,


                }).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return [];
        }
    }
    public async Task<int> GetTotalConut(CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
                .Where(o => o.IsActive)
                .CountAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return 0;
        }
    }
    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            if (item is null)
                return Result.Fail("سفارشی با این مشخص یافت نشد");
            item.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("سفارش با موفقیت حذف شد");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }

    public async Task<Result> SetFinalPrice(int id, int price, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            if (item is null)
                return Result.Fail("سفارشی با این مشخصات وجود ندارد");
            item.Price = price;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("قیمت نهایی سفارش با موفقیت تغییر کرد");

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }

    public async Task<Result> SetFinalTimeToDone(int id, DateTime timeToDone, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            if (item is null)
                return Result.Fail("سفارشی با این مشخصات وجود ندارد");
            item.TimeToDone = timeToDone;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("تاریخ نهایی سفارش با موفقیت تغییر کرد");

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }

    public async Task<List<GetOrderDto>> Search(string text, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
                .Include(o => o.Customer)
                .Include(o => o.Expert)
                .Include(o => o.SubService)
                .Where(o => o.IsActive && (!string.IsNullOrEmpty(o.Customer!.Lname) && o.Customer.Lname.Contains(text) || o.SubService!.Title.Contains(text)))
                .Select(o => new GetOrderDto
                {
                    Id = o.Id,
                    CreateAt = o.CreateAt,
                    CustomerLname = o.Customer!.Lname ?? "نامشخص",
                    SubServiceName = o.SubService!.Title,



                })
                .ToListAsync(cancellationToken);

            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return [];
        }
    }
    //
    public async Task<List<GetLastOrderDto>> GetLatestOrders(CancellationToken cancellationToken)
    {
        try
        {
            var items = await _dbContext.Orders.AsNoTracking()
                 .Include(o => o.Customer)
                 .Include(o => o.SubService)
                .OrderByDescending(o => o.CreateAt)
                .Take(10)
                .Where(o => o.IsActive)
                .Select(o => new GetLastOrderDto
                {
                    Id = o.Id,
                    CreateAt = o.CreateAt,
                    CustomerLname = o.Expert!.Lname ?? "نامشخص",
                    SubServiceName = o.SubService!.Title,

                }
                ).ToListAsync(cancellationToken);
            return items;

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return [];
        }
    }


}
