using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Categories;

namespace HomeService.Domain.Core.Contracts.AppService.EndPoint;

public interface IAdminSubserviceManagement
{
    Task<Result> Create(CreateSubServiceDto model, CancellationToken cancellationToken);
    Task<List<GetSubCategoryDto>> GetAllSubCategories(CancellationToken cancellationToken);
    Task<List<GetSubServiceDto>> GetAllSubservices(int pageNumber, CancellationToken cancellationToken);
    Task<int> GetTotalConut(CancellationToken cancellationToken);
    Task<Result> Update(UpdateSubServiceDto model, CancellationToken cancellationToken);
    Task<UpdateSubServiceDto?> GetById(int id, CancellationToken cancellationToken);
}
