using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Pages.Suggestions
{
    [Authorize(Roles = "Customer,Admin")]
    public class ChangeStatetoAcceptedModel(ISuggestionAppService suggestionAppService,IOrderAppService orderAppService) : PageModel
    {
       
        private readonly ISuggestionAppService _suggestionAppService = suggestionAppService;
        [BindProperty(SupportsGet = true)] 
        public int Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }



        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {
            if (Request.Cookies["isConfirmed"] != "True")
            {
                return Redirect("~/Account/AccessDenied");
            }
            var orderState = await orderAppService.GetLastStatusOfOrder(OrderId, cancellationToken);
            if (orderState != Domain.Core.Enums.Orders.OrderStatusEnum.WaitingForExpertSelection)
            {
                TempData["ErrorMessage"] = "سفارش در وضعیت انتخاب متخصص قرار ندارد";
                return RedirectToPage("/Orders/Index");
            }


            var result = await _suggestionAppService.ChangeStatetoAccepted(Id, cancellationToken);
            if(result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }
           return RedirectToPage("/Orders/Index");
        }
    }
}
