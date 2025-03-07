﻿using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HomeService.Endpoint.Razor.Pages.Users
{
    [Authorize(Roles = "Customer,Admin")]
    public class ProfileModel(IUserAppService userAppService, UserManager<User> userManager) : PageModel
    {
        private readonly IUserAppService _userAppService = userAppService;
        private readonly UserManager<User> _userManager = userManager;

        [BindProperty]
        public UpdateUsertDto? UserUpdateModel { get; set; } = null;
       

        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isConfirmed") != "True")
            {
                return Redirect("~/Account/AccessDenied");
            }
            var userId = _userManager.GetUserId(User);
            if (int.TryParse(userId, out var id))
            {
                UserUpdateModel = await _userAppService.GetById(id, cancellationToken);
                

                return Page();
            }
            return RedirectToPage("Login", new { area = "Account" });

        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isConfirmed") != "True")
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
