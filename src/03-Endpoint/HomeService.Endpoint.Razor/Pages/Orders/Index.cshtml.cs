using HomeService.Domain.Core.Contracts.AppService.BaseEntities;
using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Dtos.BaseEntities;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Domain.Service.AppServices.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading;

namespace HomeService.Endpoint.Razor.Pages.Orders
{
    [Authorize(Roles = "Customer,Admin")]
    public class IndexModel(IOrderAppService orderAppService, ISuggestionAppService suggestionAppService, ICommentAppService commentAppService, IUserAppService userAppService) : PageModel
    {

        private readonly IOrderAppService _orderAppService = orderAppService;
        private readonly ISuggestionAppService _suggestionAppService = suggestionAppService;
        private readonly ICommentAppService _commentAppService = commentAppService;
        private readonly IUserAppService _userAppService = userAppService;

        [BindProperty]
        public List<GettOrderOverViewDto> Orders { get; set; } = [];
        [BindProperty]
        public SuggestionDetailsDto? SuggestionDetail { get; set; } = null;
        [BindProperty]
        public static GetFinalOrderDto? OrderFactor { get; set; }

        [BindProperty]
        public CreateCommentDto? Comment { get; set; }

        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isConfirmed") != "True")
            {
                return Redirect("~/Account/AccessDenied");
            }
            try
            {
                var customerId = User.Claims.First(x => x.Type == "CustomerId").Value;
                if (int.TryParse(customerId, out var id))
                {
                    Orders = await _orderAppService.GetCustomerOrders(id, cancellationToken);

                    return Page();
                }
                return RedirectToPage("Login", new { area = "Account" });
            }
            catch
            {

                return Page();
            }

        }
        public async Task<IActionResult> OnGetSuggestionDetail(int id, CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isConfirmed") != "True")
            {
                return Redirect("~/Account/AccessDenied");
            }
            SuggestionDetail = await _suggestionAppService.GetSuggestionDetailById(id, cancellationToken);
            return new JsonResult(SuggestionDetail);
        }
        public async Task<IActionResult> OnGetOrderFinalInfo(int id, CancellationToken cancellationToken)
        {
              if (HttpContext.Session.GetString("isConfirmed") != "True")
            {
                return Redirect("~/Account/AccessDenied");
            }
            OrderFactor = await _orderAppService.GetFinalInfoById(id, cancellationToken);

            return new JsonResult(OrderFactor);
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isConfirmed") != "True")
            {
                return Redirect("~/Account/AccessDenied");
            }
            try
            {
                var customerUserId =int.Parse( User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var customerId = int.Parse(User.Claims.First(c => c.Type == "CustomerId").Value);
                var expertId= OrderFactor!.ExpertId;
                var expertUserId = OrderFactor!.ExpertUserId;
                var totalPrice = OrderFactor!.TotalPrice;
                var siteFee = OrderFactor!.SiteFee;
                var price = OrderFactor.Price;
                var withdrawResult = await _userAppService.WithdrawBalance(customerUserId, totalPrice, cancellationToken);
                if (!withdrawResult.Success)
                {
                    TempData["ErrorMessage"] = withdrawResult.Message;
                    return RedirectToPage();
                }
                var expertChargeResult = await _userAppService.ChargeBalance(expertUserId, price, cancellationToken);
                var adminChargeResult = await _userAppService.ChargeBalance(1, siteFee, cancellationToken);
                if (!expertChargeResult.Success || !adminChargeResult.Success)
                {
                    TempData["ErrorMessage"] = "مشکلی در فرایند پرداخت ایجاد شده";
                    return RedirectToPage();
                }
                var orderChangeStatusResult = await _orderAppService.ChangeStateToCompleted(OrderFactor.Id, cancellationToken);
                if (!orderChangeStatusResult.Success)
                {
                    TempData["ErrorMessage"] = orderChangeStatusResult.Message;
                    return RedirectToPage();
                }
                await _userAppService.Commit(cancellationToken);
                TempData["SuccessMessage"] = "سفارش شما با موفقیت به اتمام رسید";
                Comment!.ExpertId = expertId;
                Comment.CustomerId = customerId;
            }
            catch
            {
                TempData["ErrorMessage"] = "مشخصات مشتری یا کارشناس ناممعتبر است";
                return RedirectToPage();
            }
            if (ModelState.IsValid && Comment is not null)
            {

                var result = await _commentAppService.Create(Comment, cancellationToken);
              
                if (!result.Success)
                {
                    TempData["ErrorMessage"] = result.Message;
                }
                

            }
            else
            {
                TempData["ErrorMessage"] = "امتیاز باید بین عدد یک تا پنج و متن کامنت  باید حداکثر 255 کاراکتر باشد";
            }
            return RedirectToPage();

        }


    }
}

