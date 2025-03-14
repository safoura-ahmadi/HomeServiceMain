using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.EndPoint.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeService.EndPoint.Api.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderAppService orderAppService) : ControllerBase
    {
        [ServiceFilter(typeof(ApiKeyActionFilter))]
        [HttpGet("getOrders")]
        public async Task<IActionResult> GetOrders([FromHeader] string? apikey, CancellationToken cancellationToken,int pageNumber = 1)
        {

            if (pageNumber > 100 || pageNumber <= 0)
                pageNumber = 1;
            var items = await orderAppService.GetAll(pageNumber, cancellationToken);

            if (items == null || items.Count == 0)
                return NotFound(new { message = "سفارشی وجود ندارد" });

            return Ok(items);
        }
    }
}
