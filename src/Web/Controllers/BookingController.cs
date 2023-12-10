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
                    return StatusCode(
                        500,
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

        [HttpPatch("{bookingId}/confirm-checkup")]
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
                    return StatusCode(
                        500,
                        new
                        {
                            success = false,
                            statusCode = 500,
                            message = result
                        }
                    );

                return Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
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

        [HttpPatch("{bookingId}")]
        public IActionResult CancelBooking([FromRoute] int bookingId)
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

                var result = this.bookingService.CancelBooking(userId, bookingId);

                if (result != "Succeeded")
                    return StatusCode(
                        500,
                        new
                        {
                            success = false,
                            statusCode = 500,
                            message = result
                        }
                    );

                return Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
                        messgae = "Booking cancelled successfully.",
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

        [HttpGet]
        public IActionResult GetCountOfBookings()
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

                if (roleName != "Admin")
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with admin account."
                        }
                    );

                var result = (List<int>)this.bookingService.GetCountOfBookings();

                return Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
                        pendingRequests = result[0],
                        confirmedRequests = result[1],
                        cancelledRequests = result[2],
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

        [HttpGet("patients")]
        public IActionResult GetAllBookingsForPatient([FromQuery] int page, [FromQuery] int limit)
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

                var (bookings, totalBookingsCount) = this.bookingService.GetAllBookingsForPatient(
                    userId,
                    page,
                    limit
                );

                return Ok(
                    new
                    {
                        succuss = true,
                        statusCode = 200,
                        totalBookingsCount,
                        maxPages = (int)Math.Ceiling((decimal)totalBookingsCount / limit),
                        currentPage = page,
                        bookingsPerPage = limit,
                        bookings
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

        [HttpGet("doctors")]
        public IActionResult GetAllBookingsForDoctors([FromQuery] int page, [FromQuery] int limit)
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

                var (bookings, totalBookingsCount) = this.bookingService.GetAllBookingsForDoctor(
                    userId,
                    page,
                    limit
                );

                return Ok(
                    new
                    {
                        succuss = true,
                        statusCode = 200,
                        totalBookingsCount,
                        maxPages = (int)Math.Ceiling((decimal)totalBookingsCount / limit),
                        currentPage = page,
                        bookingsPerPage = limit,
                        bookings
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
