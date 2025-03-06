using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Dtos.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Account.Pages
{
    public class RegisterModel(IUserAppService userAppService) : PageModel
    {
        private readonly IUserAppService _userAppService = userAppService;

        [BindProperty]
        public CreateUserDto CreateModel { get; set; } = null!;
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userAppService.Register(CreateModel, cancellationToken);

                    if (result.Succeeded)
                    {
                        return RedirectToPage("Login");

                    }
                    foreach (var error in result.Errors)
                    {
                        TempData["ErrorMessage"] = error.Description;
                        return Page();
                    }
                }
                catch
                {
                    TempData["ErrorMessage"] = "مشکلی در دیتا بیس وجود دارد";

                }
               
            }

            return Page();
        }
    }
}
