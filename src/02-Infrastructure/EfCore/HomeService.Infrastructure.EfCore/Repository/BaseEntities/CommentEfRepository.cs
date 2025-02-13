using HomeService.Domain.Core.Dtos.BaseEntities;
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace HomeService.Infrastructure.EfCore.Repository.BaseEntities;

public class CommentEfRepository(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<bool> Create(CreateCommentDto item, CancellationToken cancellationToken)
    {
        if (item == null)
            return false;
        try
        {
            var comment = new Comment()
            {
                CustomerId = item.CustomerId,
                ExpertId = item.ExpertId,
                IsActive = false,
                Score = item.Score,
                Text = item.Text
            };
            await _dbContext.Comments.AddAsync(comment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return
                true;
        }
        catch
        {
            return false;

        }
    }
    public async Task<List<GetCommentDto>> GetByExpertId(int expertId, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Comments.AsNoTracking().
            Where(c => c.ExpertId == expertId && c.IsActive == true).
            Select(c => new GetCommentDto
            {

                Id = c.Id,
                Score = c.Score,
                Text = c.Text,
                IsActive = false,
                ExpertId = c.ExpertId,
                CustomerId = c.CustomerId,
            }

           ).ToListAsync(cancellationToken);
        return item;
    }
    //admin
    public async Task<List<GetCommentDto>> GetAll(CancellationToken cancellationToken)
    {
        var item = await _dbContext.Comments.AsNoTracking().
            Select(c => new GetCommentDto
            {

                Id = c.Id,
                Score = c.Score,
                Text = c.Text,
                IsActive = false,
                ExpertId = c.ExpertId,
                CustomerId = c.CustomerId,

            }

            ).ToListAsync(cancellationToken);
        return item;
    }
    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {

        var item = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (item is null)
            return false;
        item.IsActive = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;

    }

}