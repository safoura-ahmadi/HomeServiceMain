using HomeService.Domain.Core.Dtos.Users;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Infrastructure.EfCore.Repository.Users;

public class ExpertEfRepository(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<bool> IsConfirmedByAdmin(int userId, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Experts
                        .AsNoTracking()
                        .Where(e => e.UserId == userId)
                        .Select(e => e.IsConfirmed)
                        .FirstAsync(cancellationToken);
        return result;
    }
    public async Task<GetUsertDto?> GetByUserId(int userId, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Experts.AsNoTracking()
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
    public async Task<bool> Update(GetUsertDto model, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Experts.FirstOrDefaultAsync(e => e.Id == model.Id);
        if (item is null)
            return false;

        item.Fname = model.Fname;
        item.ImagePath = model.ImagePath;
        item.Lname = model.Lname;
        item.UserId = model.UserId;
        item.Biography = model.Biography;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;

    }
    //admin
    public async Task<bool> ConfirmById(int id, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Experts.FirstOrDefaultAsync(e => e.Id == id,cancellationToken);
        if (item is null)
            return false;
        item.IsConfirmed = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;

    }
    public async Task<bool> UnConfirmById(int id, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Experts.FirstOrDefaultAsync(e => e.Id == id,cancellationToken);
        if (item is null)
            return false;
        item.IsConfirmed = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;

    }

    public async Task<List<GetUsertDto>> GetAll(CancellationToken cancellationToken)
    {
        var item = await _dbContext.Experts.AsNoTracking()
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
}
