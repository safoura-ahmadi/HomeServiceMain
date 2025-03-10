using HomeService.Domain.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Account.Pages
{
    public class LogOutModel(SignInManager<User> signInManager) : PageModel
    {
        private readonly SignInManager<User> _signInManager = signInManager;

        public async Task<IActionResult> OnGet()
        {
            await _signInManager.SignOutAsync();

           

            return RedirectToPage("/Login");

        }
    }
}
