using HomeService.Domain.Core.Contracts.Repository.BaseEntities;
using HomeService.Domain.Core.Dtos.BaseEntities;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace HomeService.Infrastructure.EfCore.Repository.BaseEntities;

public class CommentEfRepository(ApplicationDbContext dbContext) : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<bool> Create(CreateCommentDto item, CancellationToken cancellationToken)
    {

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
        try
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
        catch
        {
            return [];
        }
    }
    public async Task<float> GetExpertScore(int expertId, CancellationToken cancellationToken)
    {
        try
        {
            var averageScore = await _dbContext.Comments
                .Where(c => c.ExpertId == expertId && c.Score != 0)
                .Select(c => (float?)c.Score).AverageAsync(cancellationToken);
            return averageScore ?? 0f;
        }
        catch
        {
            return 0;
        }


    }

    //admin
    public async Task<List<GetCommentDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Comments.AsNoTracking()
                .Include(c => c.Expert)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new GetCommentDto
                {

                    Id = c.Id,
                    Score = c.Score,
                    Text = c.Text,
                    IsActive = false,
                    ExpertId = c.ExpertId,
                    CustomerId = c.CustomerId,
                    ExpertLname = c.Expert!.Lname

                }

                ).ToListAsync(cancellationToken);
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
            var item = await _dbContext.Comments.AsNoTracking()
                .CountAsync(cancellationToken);
            return item;
        }
        catch
        {
            return 0;
        }
    }
    public async Task<Result> SetInActive(int id, CancellationToken cancellationToken)
    {


        try
        {
            var item = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (item is null)
                return Result.Fail("کامنتی با این مشخصات یافت نشد");
            item.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("کامنت با موفقیت  غیرفعال شد");
        }
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }

    }

    public async Task<List<GetCommentDto>> Search(string text, CancellationToken cancellationToken)
    {
        try
        {
            var items = await _dbContext.Comments.AsNoTracking()
                .Include(c => c.Expert)
                .Where(c => (!string.IsNullOrEmpty(c.Text) && c.Text.Contains(text)) ||string.IsNullOrEmpty(c.Expert!.Lname) && c.Expert.Lname.Contains(text))
                .Select(c => new GetCommentDto
                {
                    Id = c.Id,
                    Text = c.Text,
                    CustomerId = c.CustomerId,
                    ExpertId = c.ExpertId,
                    ExpertLname = c.Expert!.Lname,
                    IsActive = c.IsActive,
                    Score = c.Score,
                }).ToListAsync(cancellationToken);
            return items;

        }
        catch
        {
            return [];
        }
    }
}
