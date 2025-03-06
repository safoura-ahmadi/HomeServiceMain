using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Pages.Users
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Remove("isConfirmed");
        }
    }
}
