﻿using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Infrastructure.EfCore.Repository.Categories;

public class SubServiceEfRepository(ApplicationDbContext dbContext) : ISubServiceRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<Result> Create(CreateSubServiceDto model, CancellationToken cancellationToken)
    {
        try
        {
            var item = new SubService()
            {
                BasePrice = model.BasePrice,
                Description = model.Description,
                ImagePath = model.ImagePath,
                Title = model.Title,
                SubCategoryId = model.SubCategoryId,
                IsActive = true
            };
            await _dbContext.SubServices.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("سرویس جدید با موفقیت ایجاد شد");

        }
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<List<GetSubServiceDto>> GetBySubCategoryId(int subcategoryId, CancellationToken cancellationToken)
    {
        var item = await _dbContext.SubServices.AsNoTracking()
             .Where(s => s.IsActive && s.SubCategoryId == subcategoryId)
             .Select(s => new GetSubServiceDto
             {
                 Id = s.Id,
                 Title = s.Title,
                 Description = s.Description,
                 BasePrice = s.BasePrice,
                 ImagePath = s.ImagePath,

             }
             ).ToListAsync(cancellationToken);
        return item;
    }
    public async Task<List<GetSubServiceDto>> Search(string text, CancellationToken cancellationToken
        )
    {
        var item = await _dbContext.SubServices.AsNoTracking()
            .Where(s => s.IsActive && s.Title.Contains(text) || s.Description.Contains(text))
            .Include(s => s.SubCategory)
            .Select(s => new GetSubServiceDto
            {
                Id = s.Id,
                Title = s.Title,
                BasePrice = s.BasePrice,
                Description = s.Description,
                ImagePath = s.ImagePath,
                SubCategoryTitle = s.SubCategory!.Title

            }).ToListAsync(cancellationToken);
        return item;
    }
    public async Task<int> GetBasePrice(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubServices.AsNoTracking()
                .Where(s => s.Id == id && s.IsActive)
                .Select(s => s.BasePrice)
                .FirstAsync(cancellationToken);
            return item;
        }
        catch
        {
            return 0;
        }
    }

    //admin
    public async Task<Result> Update(CreateSubServiceDto model, CancellationToken cancellationToken)

    {
        try
        {
            var item = await _dbContext.SubServices.FirstOrDefaultAsync(s => s.Id == model.Id && s.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("هوم سرویسی با این مشخصات وجود ندارد");
            item.Title = model.Title;
            item.Description = model.Description;
            item.ImagePath = model.ImagePath;
            item.BasePrice = model.BasePrice;
            item.SubCategoryId = model.SubCategoryId;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("ویرایش هوم سرویس با موفقیت انجام شد");
        }
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubServices.FirstOrDefaultAsync(s => s.Id == id && s.IsActive, cancellationToken);
            if (item is null)
                return Result.Fail("هوم سرویسی با این مشخصات وجود ندارد");
            item.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok("هوم سرویس با موفقیت اپدیت شد");
        }
        catch
        {
            return Result.Fail("مشکلی در دیتا بیس وجود دارد");
        }
    }
    public async Task<List<GetSubServiceDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.SubServices.AsNoTracking()
                 .Where(s => s.IsActive)
                 .Include(S => S.SubCategory)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .Select(s => new GetSubServiceDto
                 {
                     Id = s.Id,
                     Title = s.Title,
                     Description = s.Description,
                     BasePrice = s.BasePrice,
                     ImagePath = s.ImagePath,
                     SubCategoryTitle = s.SubCategory!.Title
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
            var item = await _dbContext.SubServices.AsNoTracking()
                .CountAsync(cancellationToken);
            return item;
        }
        catch
        {
            return 0;
        }
    }
}
