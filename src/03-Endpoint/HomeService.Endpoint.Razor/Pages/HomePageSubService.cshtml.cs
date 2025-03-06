using Framework;
using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading;

namespace HomeService.Endpoint.Razor.Pages
{
    public class HomePageSubServiceModel(ISubServiceAppService subServiceAppService, IOrderAppService orderAppService) : PageModel
    {
        private readonly ISubServiceAppService _subServiceAppService = subServiceAppService;
        private readonly IOrderAppService _orderAppService = orderAppService;

        [BindProperty]
        public List<GetSubServiceDto> Subservices { get; set; } = [];
        [BindProperty]
        public CreateOrderDto Order { get; set; } = new();
        [BindProperty]
        public static int SubCategoryId { get; set; }
        [BindProperty]
        public string PersianDate { get; set; } = null!;
        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isConfirmed") != "True")
            {
                return RedirectToPage("AccessDenied", new { area = "Account" });
            }

            SubCategoryId = id;
            Subservices = await _subServiceAppService.GetBySubCategoryId(id, cancellationToken);
            return Page();
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)

        {
            if (HttpContext.Session.GetString("isConfirmed") != "True")
            {
               return RedirectToPage("AccessDenied", new { area = "Account" });
            }
            Subservices = await _subServiceAppService.GetBySubCategoryId(SubCategoryId, cancellationToken);
            if (!User.IsInRole("Customer"))
            {
                TempData["ErrorMessage"] = "امکان ثبت سفارش فقط برای نقش مشتری فعال است";
                return Page();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var date = PersianDate.ToGregorianDate();
                    if (date <= DateTime.Now)
                    {
                        TempData["ErrorMessage"] = "تاریخ انتخابی شما نامعتبر است";
                        return Page();
                    }
                    Order!.SubServiceId = SubCategoryId;
                    Order.TimeToDone = date;
                    Order.CustomerId = int.Parse(User.Claims.First(x => x.Type == "CustomerId").Value);
                    var result = await _orderAppService.Create(Order, cancellationToken);

                    if (result.Success)
                    {
                        TempData["SuccessMessage"] = "ثبت سفارش با موفقیت انجام شد";

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
