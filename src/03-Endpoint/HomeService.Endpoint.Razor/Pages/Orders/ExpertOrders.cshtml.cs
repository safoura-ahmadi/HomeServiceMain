using Framework;
using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Domain.Service.AppServices.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace HomeService.Endpoint.Razor.Pages.Orders
{
    [Authorize(Roles = "Expert,Admin")]
    public class ExpertOrdersModel(IOrderAppService orderAppService, IUserAppService userAppService, IExpertSubServiceAppservice expertSubServiceAppservice, ISuggestionAppService suggestionAppService) : PageModel
    {
        [BindProperty]
        public List<GetOrderDto> Orders { get; set; } = [];
        [BindProperty]
        public string PersianDate { get; set; } = null!;
        [BindProperty]
        public SuggestionDto Suggestion { get; set; } = new();
        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {
            if (Request.Cookies["isConfirmed"] != "True")
            {
                return RedirectToPage("AccessDenied", new { area = "Account" });
            }
            if (User.IsInRole("Expert"))
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var expertId = int.Parse(User.Claims.First(c => c.Type == "ExpertId").Value);
                var cityId = await userAppService.GetCityId(userId, cancellationToken);
                var skills = await expertSubServiceAppservice.GetSubServicesByExpertId(expertId, cancellationToken);
                Orders = await orderAppService.GetAvailableOrdersForExpert(expertId, cityId, skills, cancellationToken);
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)

        {
            if (Request.Cookies["isConfirmed"] != "True")
            {
                return RedirectToPage("AccessDenied", new { area = "Account" });
            }
            if (User.IsInRole("Expert"))
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var expertId = int.Parse(User.Claims.First(c => c.Type == "ExpertId").Value);
                var cityId = await userAppService.GetCityId(userId, cancellationToken);
                var skills = await expertSubServiceAppservice.GetSubServicesByExpertId(expertId, cancellationToken);
                Orders = await orderAppService.GetAvailableOrdersForExpert(expertId, cityId, skills, cancellationToken);
            }

            if (!User.IsInRole("Expert"))
            {
                TempData["ErrorMessage"] = "امکان ثبت پیشنهاد فقط برای نقش کارشناس فعال است";
                return Page();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var date = PersianDate.ToGregorianDate();
                    if (date < DateTime.Now)
                    {
                        TempData["ErrorMessage"] = "تاریخ انتخابی شما نامعتبر است";
                        return Page();
                    }
                   
                    Suggestion.TimeToDone = date;
                    Suggestion.ExpertId = int.Parse(User.Claims.First(x => x.Type == "ExpertId").Value);
                    var result = await suggestionAppService.Create(Suggestion, cancellationToken);

                    if (result.Success)
                    {
                        var orderChangeStatusResult = await orderAppService.ChangeStateToWaitingForExpertSelection(Suggestion.OrderId, cancellationToken);
                        if (!orderChangeStatusResult.Success)
                        {
                            TempData["ErrorMessage"] = orderChangeStatusResult.Message;
                         
                        }
                        TempData["SuccessMessage"] = "ثبت پیشنهاد با موفقیت انجام شد";
                        return RedirectToPage();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = result.Message;
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

