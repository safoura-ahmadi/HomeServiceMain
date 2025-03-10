using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Pages.Orders
{
    [Authorize(Roles = "Customer,Admin")]
    public class ChangeOrderStatusModel : PageModel
    {

        public IActionResult OnGet()
        {
            if (Request.Cookies["isConfirmed"] != "True")
            {
                return Redirect("~/Account/AccessDenied");
            }
            return Page();
        }
    }
}
