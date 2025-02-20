using HomeService.Domain.Core.Contracts.AppService.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Orders
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel(IOrderAppService appService) : PageModel
    {
        private readonly IOrderAppService _appService = appService;

        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            var result = await _appService.Delete(id, cancellationToken);
            TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
            return RedirectToPage("Index");
        }
    }
}
