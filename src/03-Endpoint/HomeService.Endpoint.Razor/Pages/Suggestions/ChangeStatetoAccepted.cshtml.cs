using HomeService.Domain.Core.Contracts.AppService.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Pages.Suggestions
{
    [Authorize(Roles = "Customer,Admin")]
    public class ChangeStatetoAcceptedModel(ISuggestionAppService suggestionAppService) : PageModel
    {
       
        private readonly ISuggestionAppService _suggestionAppService = suggestionAppService;
        [BindProperty(SupportsGet = true)] 
        public int Id { get; set; }
       


        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isConfirmed") != "True")
            {
                return Redirect("~/Account/AccessDenied");
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
