
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace HomeService.Domain.Core.Contracts.AppService.Categories;
public interface ICategoryAppService
{
    Task<Result> Delete(int id, CancellationToken cancellationToken);
    Task<Result> Update(int id, string title, IFormFile imageFile, CancellationToken cancellationToken);
    Task<List<GetCategoryForAdminPageDto>> GetAll(CancellationToken cancellationToken);
    Task<Result> Create(string title, IFormFile imageFile, CancellationToken cancellationToken);
    Task<Result> Update(UpdateCategoryDto model, CancellationToken cancellationToken);
    Task<UpdateCategoryDto?> GetById(int id, CancellationToken cancellationToken);
    Task<List<GetCategoryForMainPageDto>> GetAllForMainPage(CancellationToken cancellationToken);
}
