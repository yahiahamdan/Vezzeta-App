using Application.Dtos;
using Application.Interfaces.Helpers;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/discount-code")]
    [ApiController]
    public class DiscountController : Controller
    {
        private readonly IDiscountService discountService;
        private readonly IJwtHelpService jwtHelpService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DiscountController(
            IDiscountService discountService,
            IJwtHelpService jwtHelpService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            this.discountService = discountService;
            this.jwtHelpService = jwtHelpService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult CreateNewDiscount(DiscountDto discountDto)
        {
            try
            {
                string accessToken = this.httpContextAccessor.HttpContext.Request.Cookies[
                    "accessToken"
                ];

                if (accessToken == null)
                    return Unauthorized(
                        new
                        {
                            success = false,
                            statusCode = 401,
                            message = "Unauthorized"
                        }
                    );

                var decodedToken = this.jwtHelpService.DecodeToken(accessToken);

                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Admin")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden"
                        }
                    );
                }

                if (!ModelState.IsValid)
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            messgae = ModelState.ValidationState
                        }
                    );

                var result = this.discountService.CreateNewDiscount(discountDto);

                if (result != "Succeeded")
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            result,
                        }
                    );

                return Created(
                    "Discount created successfully",
                    new
                    {
                        success = true,
                        statusCode = 201,
                        message = "Discount created successfully"
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    403,
                    new
                    {
                        success = false,
                        statusCode = 403,
                        message = "Forbidden"
                    }
                );
            }
        }
    }
}
