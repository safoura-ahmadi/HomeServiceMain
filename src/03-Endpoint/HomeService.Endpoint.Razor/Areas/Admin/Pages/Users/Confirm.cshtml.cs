using HomeService.Domain.Core.Contracts.AppService.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class ConfirmModel(IUserAppService appService) : PageModel
    {
        private readonly IUserAppService _appService = appService;

        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            var result = await _appService.ConfirmById(id, cancellationToken);
            TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
            return RedirectToPage("Index");
        }
    }
}
