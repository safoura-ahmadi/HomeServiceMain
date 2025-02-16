using HomeService.Domain.Core.Contracts.Repository.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;


namespace HomeService.Infrastructure.EfCore.Repository.Orders;

public class OrderEfRepository(ApplicationDbContext dbContext) : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<bool> Create(CreateOrderDto order, CancellationToken cancellationToken)
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
            await _dbContext.Orders.AddAsync(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;

        }
        catch
        {
            return false;
        }
    }
    public async Task<List<GetOrderDto>> GetForExpert(int cityId, int subserviceId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
                .Where(o => o.IsActive && o.Customer.CityId == cityId &&
                o.SubServiceId == subserviceId &&
                (o.Status == Domain.Core.Enums.Orders.OrderStatusEnum.WaitingForExpertOffer ||
                o.Status == Domain.Core.Enums.Orders.OrderStatusEnum.WaitingForExpertSelection))
                .Select(o => new GetOrderDto
                {
                    Id = o.Id,
                    CreateAt = DateTime.UtcNow,
                    CustomerId = o.CustomerId,
                    CustomerLname = o.Customer.Lname,
                    Description = o.Description,
                    Price = o.Price,
                    TimeToDone = o.TimeToDone
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch
        {
            return [];
        }


    }
    public async Task<bool> ChangeStateToWaitingForExpertSelection(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstAsync(o => o.Id == id, cancellationToken);
            item.Status = Domain.Core.Enums.Orders.OrderStatusEnum.WaitingForExpertSelection;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<bool> ChangeStateToWorkCompletedAndPaid(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstAsync(o => o.Id == id, cancellationToken);
            item.Status = Domain.Core.Enums.Orders.OrderStatusEnum.WorkCompletedAndPaid;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<bool> ChangeStateToExpertArrivedAtLocation(int id, DateTime FinaltimeToDone, int finalPrice, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstAsync(o => o.Id == id, cancellationToken);
            item.Status = Domain.Core.Enums.Orders.OrderStatusEnum.ExpertArrivedAtLocation;
            item.TimeToDone = FinaltimeToDone;
            item.Price = finalPrice;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
    //admin
    public async Task<List<GetOrderDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.AsNoTracking()
                .Where(o => o.IsActive)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new GetOrderDto
                {
                    Id = o.Id,
                    CreateAt = DateTime.UtcNow,
                    CustomerId = o.CustomerId,
                    CustomerLname = o.Customer.Lname,
                    Description = o.Description,
                     Price = o.Price,
                    TimeToDone = o.TimeToDone
                }).ToListAsync(cancellationToken);
            return item;
        }
        catch
        {
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
        catch
        {
            return 0;
        }
    }
    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Orders.FirstAsync(o => o.Id == id, cancellationToken);
            item.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

}
