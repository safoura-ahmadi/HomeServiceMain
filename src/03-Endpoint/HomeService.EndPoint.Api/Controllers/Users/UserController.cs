using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Dtos.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace HomeService.EndPoint.Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserAppService userAppService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<IdentityResult>> Register(CreateUserDto user, CancellationToken cancellationToken)
        {
          

            if (!ModelState.IsValid)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Invalid input",
                    Detail = "The input data is invalid.",
                    Status = StatusCodes.Status400BadRequest
                });
            }

            var result = await userAppService.Register(user, cancellationToken);

            if (result.Succeeded)
            {
                return Ok(new { message = "کاربر با موفقیت اضافه شد"});
            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Registration failed",
                    Detail = "The user registration failed.",
                    Status = StatusCodes.Status400BadRequest,
                    Extensions = { { "errors", result.Errors } }
                });
            }
        }
    }
}
