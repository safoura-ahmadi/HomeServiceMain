using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Entities.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace HomeService.Endpoint.Razor.Areas.Account.Pages
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "وارد کردن نام کاربری الزامی است")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "وارد کردن پسورد الزامی است")]
        public string Password { get; set; } = null!;

    }

    public class LoginModel(IUserAppService userAppService) : PageModel
    {

        [BindProperty]
        public LoginViewModel PageModel { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await userAppService.Login(PageModel.Username, PageModel.Password, cancellationToken);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = "نام کاربری با رمز عبور اشتباه است";
                return Page();
            }

            var id = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var status = await userAppService.IsConfirmedByAdmin(id, cancellationToken);

            if (status != Domain.Core.Enums.Users.UserStatusEnum.Accepted)
            {
                TempData["ErrorMessage"] = "شما هنوز توسط ادمین تایید نشده اید ";
                Response.Cookies.Append("isConfirmed", "False", new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(30), 
                    HttpOnly = true,  
                    Secure = true,  
                    SameSite = SameSiteMode.Strict 
                });
                return Page();
            }

            Response.Cookies.Append("isConfirmed", "True", new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(30),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            var userRole = User.Claims.First(x => x.Type == ClaimTypes.Role).Value;

            return userRole switch
            {
                "Admin" => RedirectToPage("Index", new { area = "Admin" }),
                "Customer" => RedirectToPage("Index"),
                "Expert" => RedirectToPage("Index"),
                _ => RedirectToPage("Login"),
            };
        }

    }
}
