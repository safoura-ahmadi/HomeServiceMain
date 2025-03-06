using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Orders
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel(IOrderAppService appService) : PageModel
    {
        private readonly IOrderAppService _appService = appService;
        [BindProperty]
        public GetOrderDto? DetailModel { get; set; }
        public async Task OnGet(int id,CancellationToken cancellationToken)
        {
            DetailModel = await _appService.GetById(id,cancellationToken);
        } 
    }
}
