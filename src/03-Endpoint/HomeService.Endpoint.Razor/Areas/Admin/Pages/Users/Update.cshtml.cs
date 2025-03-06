using HomeService.Domain.Core.Contracts.AppService.BaseEntities;
using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities.BaseEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class UpdateModel(IUserAppService userAppService) : PageModel
    {
        private readonly IUserAppService _userAppService = userAppService;
        

        [BindProperty]
        public UpdateUsertDto? UserUpdateModel { get; set; } = null;

        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
          
            UserUpdateModel = await _userAppService.GetById(id, cancellationToken);
        
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
           
            if (ModelState.IsValid || UserUpdateModel is not { })
            {
                try
                {

                   
                    var result = await _userAppService.Update(UserUpdateModel!, cancellationToken);

                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "مشخصات کاربر با موفقیت تغییر یافت";
                         return RedirectToPage("Index");
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
                return Page();
             
            }
            return Page();
          
        }
        

    }
}
