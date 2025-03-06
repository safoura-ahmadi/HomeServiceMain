using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Pages.Orders
{
    [Authorize(Roles = "Customer,Admin")]
    public class OrderDetailsModel(IOrderAppService orderAppService) : PageModel
    {
        private readonly IOrderAppService _orderAppService = orderAppService;

        [BindProperty]
        public GetOrderDto? DetailModel { get; set; }
        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isConfirmed") != "True")
            {
                return Redirect("~/Account/AccessDenied");
            }
            DetailModel = await _orderAppService.GetById(id, cancellationToken);
            return Page();
        }
    }
}
