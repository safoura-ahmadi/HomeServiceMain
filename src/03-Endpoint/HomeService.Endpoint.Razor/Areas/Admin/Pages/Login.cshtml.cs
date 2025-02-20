using HomeService.Domain.Core.Contracts.AppService.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages
{
    public class LoginModel(IUserAppService userAppService) : PageModel
    {
        private readonly IUserAppService _userAppService = userAppService;
        [BindProperty]
        public string UserName { get; set; } = null!;
        [BindProperty]
        public string Password { get; set; } = null!;
        public void OnGet()
        {

           
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var isLogin = await _userAppService.Login(UserName, Password, cancellationToken);
            if (isLogin.Succeeded)
            {
               
                return RedirectToPage("Index");
            }
            TempData["ErrorMessage"] = "نام کاربری یا رمز عبور اشتباه است";
            return Page();


        }
    }
}
