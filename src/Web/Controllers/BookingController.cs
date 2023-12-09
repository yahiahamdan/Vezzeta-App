using Application.Dtos;
using Application.Interfaces.Helpers;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IJwtHelpService jwtHelpService;
        private readonly IBookingService bookingService;

        public BookingController(
            IHttpContextAccessor httpContextAccessor,
            IJwtHelpService jwtHelpService,
            IBookingService bookingService
        )
        {
            this.bookingService = bookingService;
            this.jwtHelpService = jwtHelpService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("{appointmentTimeId:int}")]
        public IActionResult CreateNewBooking(
            DiscountCodeCouponDto discountCodeCouponDto,
            [FromRoute] int appointmentTimeId
        )
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

                string userId = decodedToken.Claims.First(claim => claim.Type == "UserId").Value;

                if (roleName != "Patient")
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with patient account."
                        }
                    );

                if (!ModelState.IsValid)
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            message = ModelState.ValidationState
                        }
                    );
                var result = this.bookingService.CreateNewBooking(
                    userId,
                    appointmentTimeId,
                    discountCodeCouponDto
                );

                if (result != "Succeeded")
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 500,
                            message = result
                        }
                    );

                return Created(
                    "Booking created successfully.",
                    new
                    {
                        success = true,
                        statusCode = 201,
                        messgae = "Booking created successfully.",
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
                        messgae = ex.Message
                    }
                );
            }
        }

        [HttpPost("{bookingId}/confirm-checkup")]
        public IActionResult ConfirmBooking([FromRoute] int bookingId)
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

                string userId = decodedToken.Claims.First(claim => claim.Type == "UserId").Value;

                if (roleName != "Doctor")
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with doctor account."
                        }
                    );

                var result = this.bookingService.ConfirmBooking(userId, bookingId);

                if (result != "Succeeded")
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 500,
                            message = result
                        }
                    );

                return Created(
                    "Booking created successfully.",
                    new
                    {
                        success = true,
                        statusCode = 201,
                        messgae = "Booking confirmed successfully.",
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
                        messgae = ex.Message
                    }
                );
            }
        }
    }
}
