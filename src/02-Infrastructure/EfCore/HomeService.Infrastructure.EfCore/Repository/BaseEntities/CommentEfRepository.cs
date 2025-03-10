using HomeService.Domain.Core.Contracts.Repository.BaseEntities;
using HomeService.Domain.Core.Dtos.BaseEntities;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Enums.BaseEntities;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace HomeService.Infrastructure.EfCore.Repository.BaseEntities;

public class CommentEfRepository(ApplicationDbContext dbContext, ILogger<CommentEfRepository> logger) : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<CommentEfRepository> _logger = logger;

    public async Task<Result> Create(CreateCommentDto item, CancellationToken cancellationToken)
    {

        try
        {
            var comment = new Comment()
            {
                CustomerId = item.CustomerId,
                ExpertId = item.ExpertId??0,
                Status = CommentStatusEnum.Pending,
                Score = item.Score,
                Text = item.Text,
                CreateAt = DateTime.Now,

            };
            await _dbContext.Comments.AddAsync(comment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("کامنت با موفقیت ثبت شد");

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CommentEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");

        }
    }
    public async Task<List<GetCommentDto>> GetByExpertId(int expertId, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Comments.AsNoTracking().
                Include(c => c.Expert)
                .ThenInclude(e => e!.User).
                Where(c => c.ExpertId == expertId && c.Status == CommentStatusEnum.Accepted).
                Select(c => new GetCommentDto
                {

                    Id = c.Id,
                    Score = c.Score,
                    Text = c.Text,
                    CreateAt = c.CreateAt,
                    ExpertLname = c.Expert!.User!.Lname ?? "نامشخص",
                    CustomerLname = c.Customer!.User!.Lname??"نامشخص"
                }

               ).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CommentEfRepository", ex.Message);
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
            return averageScore ?? 5f;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CommentEfRepository", ex.Message);
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
                .Where(c => c.Status != CommentStatusEnum.Rejected)
                .Select(c => new GetCommentDto
                {

                    Id = c.Id,
                    Score = c.Score,
                    Text = c.Text,
                    Status = c.Status,
                    ExpertLname = c.Expert!.User!.Lname ?? "نامشخص",
                    CreateAt = c.CreateAt

                }

                ).ToListAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CommentEfRepository", ex.Message);
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
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CommentEfRepository", ex.Message);
            return 0;
        }
    }
    public async Task<Result> ChangeStatusToRejected(int id, CancellationToken cancellationToken)
    {


        try
        {
            var item = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (item is null)
                return Result.Fail("کامنتی با این مشخصات یافت نشد");
            item.Status = CommentStatusEnum.Rejected;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("کامنت با موفقیت  غیرفعال شد");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CommentEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }

    }

    public async Task<List<GetCommentDto>> Search(string text, CancellationToken cancellationToken)
    {
        try
        {
            var items = await _dbContext.Comments.AsNoTracking()
                .Include(c => c.Expert)
                .Where(c => (!string.IsNullOrEmpty(c.Text) && c.Text.Contains(text)) || (string.IsNullOrEmpty(c.Expert!.User!.Lname) && c.Expert!.User!.Lname!.Contains(text)) && c.Status != CommentStatusEnum.Rejected)
                .Select(c => new GetCommentDto
                {
                    Id = c.Id,
                    Text = c.Text,
                    ExpertLname = c.Expert!.User!.Lname ?? "نامشخص",
                    Status = c.Status,
                    Score = c.Score,
                    CreateAt = c.CreateAt,

                }).ToListAsync(cancellationToken);
            return items;

        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CommentEfRepository", ex.Message);
            return [];
        }
    }

    public async Task<Result> ChangeStatusToAccepted(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (item is null)
                return Result.Fail("کامنتی با این مشخصات یافت نشد");
            item.Status = CommentStatusEnum.Accepted;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("کامنت با موفقیت  غیرفعال شد");
        }
        catch (Exception ex)
        {
            _logger.LogError("This Error Raised in {RepositoryName} by {ErrorMessage}", "CommentEfRepository", ex.Message);
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
}
