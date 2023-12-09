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
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

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
                            message = "Forbidden. Should log in with admin account."
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

        [HttpPut("{discountId:int}")]
        public IActionResult UpdateDiscountCode(DiscountDto discountDto, [FromRoute] int discountId)
        {
            try
            {
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

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
                            message = "Forbidden. Should log in with admin account."
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

                var result = this.discountService.UpdateDiscountCode(discountDto, discountId);

                if (result != "Succeeded")
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            result,
                        }
                    );

                return Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
                        message = "Discount updated successfully"
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        success = false,
                        statusCode = 500,
                        message = ex.Message
                    }
                );
            }
        }

        [HttpDelete("{discountId:int}")]
        public IActionResult DeleteDiscountCode([FromRoute] int discountId)
        {
            try
            {
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

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
                            message = "Forbidden. Should log in with admin account."
                        }
                    );
                }
                var result = this.discountService.DeleteDiscount(discountId);

                if (result != "Succeeded")
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            result,
                        }
                    );

                return Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
                        message = "Discount deleted successfully"
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        success = false,
                        statusCode = 500,
                        message = ex.Message
                    }
                );
            }
        }

        [HttpPatch("{discountId:int}")]
        public IActionResult DeActivateDiscountCode([FromRoute] int discountId)
        {
            try
            {
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

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
                            message = "Forbidden. Should log in with admin account."
                        }
                    );
                }
                var result = this.discountService.DeActivateDiscount(discountId);

                if (result != "Succeeded")
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            result,
                        }
                    );

                return Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
                        message = "Discount code deactivated"
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        success = false,
                        statusCode = 500,
                        message = ex.Message
                    }
                );
            }
        }
    }
}
