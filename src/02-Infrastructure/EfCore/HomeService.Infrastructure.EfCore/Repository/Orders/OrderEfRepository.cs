using HomeService.Domain.Core.Contracts.Repository.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Entities.Users;
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
    public async Task<List<GetOrderDto>> GetAvailableOrdersForExpert(int expertId, int cityId, List<int> subserviceIds, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
                  .Where(o => o.IsActive &&
                (
                    (o.ExpertId.HasValue && o.ExpertId == expertId) // حالت اول
                    ||
                    ( // حالت دوم
                        o.Customer!.User!.CityId == cityId &&
                        subserviceIds.Contains(o.SubServiceId) &&
                        (o.Status == OrderStatusEnum.WaitingForExpertOffer ||
                         o.Status == OrderStatusEnum.WaitingForExpertSelection)
                    )
                )
            )

                .Select(o => new GetOrderDto
                {
                    Id = o.Id,
                    CreateAt = o.CreateAt,
                    SubServiceName = o.SubService!.Title,
                    Status = o.Status,
                    SubServiceId = o.SubServiceId
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
                 .ThenInclude(c => c!.User)
                 .Include(o => o.SubService)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new GetAllOrderDto
                {
                    Id = o.Id,
                    CreateAt = o.CreateAt,
                    Status = o.Status,
                    SubServiceName = o.SubService!.Title,
                    CustomerLname = o.Customer!.User!.Lname ?? "نامشخص",
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
                .Where(o => o.IsActive && (!string.IsNullOrEmpty(o.Customer!.User!.Lname) && o.Customer.User!.Lname.Contains(text) || o.SubService!.Title.Contains(text)))
                .Select(o => new GetOrderDto
                {
                    Id = o.Id,
                    CreateAt = o.CreateAt,
                    CustomerLname = o.Customer!.User!.Lname ?? "نامشخص",
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
    public async Task<List<GettOrderOverViewDto>> GetLatestOrders(CancellationToken cancellationToken)
    {
        try
        {
            var items = await _dbContext.Orders.AsNoTracking()
                 .Include(o => o.Customer)
                 .ThenInclude(c => c.User)
                 .Include(o => o.SubService)
                .OrderByDescending(o => o.CreateAt)
                .Take(10)
                .Where(o => o.IsActive)
                .Select(o => new GettOrderOverViewDto
                {
                    Id = o.Id,
                    CreateAt = o.CreateAt,
                    CustomerLname = o.Expert!.User!.Lname ?? "نامشخص",
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

    public async Task<GetOrderDto?> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
                .Where(o => o.Id == id && o.IsActive)
                .Include(o => o.Expert)
                .Include(o => o.Customer)
                .Include(o => o.SubService)
                .Include(o => o.Images)
                .Select(o => new GetOrderDto
                {
                    Id = o.Id,
                    CustomerLname = o.Customer!.User!.Lname ?? "نامشخص",
                    CreateAt = o.CreateAt,
                    Description = o.Description,
                    Images = o.Images,
                    Price = o.Price,
                    Status = o.Status,
                    SubServiceName = o.SubService!.Title,
                    TimeToDone = o.TimeToDone
                }).FirstOrDefaultAsync(cancellationToken);
            return item;

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return null;
        }
    }

    public async Task<int> GetActiveOrdersCountByExpert(int expertId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
                .Where(o => o.ExpertId == expertId && o.IsActive)
                .CountAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);

            return 0;
        }
    }

    public async Task<int> GetActiveOrdersCountByCustomer(int customerId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
               .Where(o => o.CustomerId == customerId && o.IsActive)
               .CountAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return 0;
        }
    }

    public async Task<List<GettOrderOverViewDto>> GetCustomerOrders(int CustomerId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
                .Include(o => o.Customer)
                .Include(o => o.SubService)
                .Where(o => o.CustomerId == CustomerId && o.IsActive)
                .Select(o => new GettOrderOverViewDto
                {
                    Id = o.Id,
                    CreateAt = o.CreateAt,
                    CustomerLname = o.Customer!.User!.Lname ?? "نا مشخص",
                    SubServiceName = o.SubService!.Title,
                    Status = o.Status
                }).ToListAsync(cancellationToken);
            return item;

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return [];
        }
    }

    public async Task<Result> ChangeStateToCompleted(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id && o.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("سفارشی با این مشخص یافت نشد");
            item.Status = OrderStatusEnum.Completed;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("وضعیت سفارش با موفقیت تغییر یافت");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }



    public async Task<Result> Update(UpdateOrderDto model, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == model.Id && o.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("سفارشی با این مشخصات وجود ندارد");
            item.Status = OrderStatusEnum.WorkCompletedAndPaid;
            item.ExpertId = model.ExpertId;
            item.Price = model.Price;
            item.TimeToDone = model.TimeToDone;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("سفارش شما با موفقیت آپدیت شد");



        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }

    public async Task<GetFinalOrderDto?> GetFinalInfoById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
                .Include(o => o.Expert)
                .ThenInclude(o => o!.User)
                .Where(o => o.Id == id && o.IsActive)
                .Select(o => new GetFinalOrderDto
                {
                    Id = o.Id,
                    ExpertLname = o.Expert!.User!.Lname ?? "نامشخص",
                    Price = o.Price,
                    ExpertId = o.ExpertId,
                    ExpertUserId = o.Expert.UserId

                }).FirstOrDefaultAsync(cancellationToken);
            return item;

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "OrderEfRepositor", ex.Message);
            return null;
        }
    }
}
