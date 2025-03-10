using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HomeService.Endpoint.Razor.Pages.Users
{
    [Authorize]
    public class ProfileModel(IUserAppService userAppService, UserManager<User> userManager, IExpertSubServiceAppservice expertSubServiceAppservice) : PageModel
    {
        private readonly IUserAppService _userAppService = userAppService;
        private readonly UserManager<User> _userManager = userManager;

        [BindProperty]
        public UpdateUsertDto? UserUpdateModel { get; set; } = null;

        [BindProperty]
        public List<int> SubServiceIds { get; set; } = [];
        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {
            if (Request.Cookies["isConfirmed"] != "True")
            {
                return Redirect("~/Account/AccessDenied");
            }
            var userId = _userManager.GetUserId(User);
            if (int.TryParse(userId, out var id))
            {
                UserUpdateModel = await _userAppService.GetById(id, cancellationToken);
                if (User.IsInRole("Expert"))
                {
                    var expertId = int.Parse(User.Claims.First(c => c.Type == "ExpertId").Value);
                    SubServiceIds = await expertSubServiceAppservice.GetSubServicesByExpertId(expertId, cancellationToken);

                }

                return Page();
            }
            return RedirectToPage("Login", new { area = "Account" });

        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (Request.Cookies["isConfirmed"] != "True")
            {
                return Redirect("~/Account/AccessDenied");
            }

            if (ModelState.IsValid || UserUpdateModel is not { })
            {
                try
                {


                    var result = await _userAppService.Update(UserUpdateModel!, cancellationToken);

                    if (result.Succeeded)
                    {
                        if (User.IsInRole("Expert"))
                        {
                            var expertId = int.Parse(User.Claims.First(c => c.Type == "ExpertId").Value);
                           
                                var skillResult = await expertSubServiceAppservice.Create(expertId, SubServiceIds, cancellationToken);
                                if (!skillResult)

                                    TempData["ErrorMessage"] = "در فرایند بروز رسانی  مهارت ها مشکلی ایجاد شده";
                            

                        }

                        TempData["SuccessMessage"] = "مشخصات کاربر با موفقیت تغییر یافت";
                        return RedirectToPage();

                    }
                    foreach (var error in result.Errors)
                    {
                        TempData["ErrorMessage"] = error.Description;

                    }
                }
                catch
                {
                    TempData["ErrorMessage"] = "مشکلی در دیتا بیس بوجود امده";

                }


            }

            return Page();

        }

    }
}
