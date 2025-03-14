using HomeService.Domain.Core.Contracts.AppService.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeService.EndPoint.Api.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategories(ISubCategoryAppService subCategoryAppService) : ControllerBase
    {
        [HttpGet("getAllWithService")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var items = await subCategoryAppService.GetAllWithService(cancellationToken);
            return Ok(items);
        }
    }
}
