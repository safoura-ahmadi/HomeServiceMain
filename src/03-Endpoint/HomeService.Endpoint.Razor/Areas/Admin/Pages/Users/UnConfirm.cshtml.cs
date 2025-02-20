using HomeService.Domain.Core.Contracts.AppService.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class UnConfirmModel(IUserAppService _appService) : PageModel
    {
        private readonly IUserAppService _appService = _appService;

        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            var result = await _appService.UnConfirmById(id, cancellationToken);
            TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
            return RedirectToPage("Index");
        }
    }
}
