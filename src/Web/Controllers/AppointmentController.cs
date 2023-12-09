using Application.Dtos;
using Application.Interfaces.Helpers;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService appointmentService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IJwtHelpService jwtHelpService;

        public AppointmentController(
            IAppointmentService appointmentService,
            IHttpContextAccessor httpContextAccessor,
            IJwtHelpService jwtHelpService
        )
        {
            this.jwtHelpService = jwtHelpService;
            this.httpContextAccessor = httpContextAccessor;
            this.appointmentService = appointmentService;
        }

        [HttpPost]
        public IActionResult CreateNewAppointment([FromBody] AppointmentDto appointmentDto)
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

                string doctorId = decodedToken.Claims.First(claim => claim.Type == "UserId").Value;
                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Doctor")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with doctor account."
                        }
                    );
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            messgae = ModelState.ValidationState
                        }
                    );
                }
                var distinctDays = appointmentDto.Appointments
                    .Select(appointment => appointment.Day)
                    .Distinct();

                if (distinctDays.Count() != appointmentDto.Appointments.Count)
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            message = "Duplicate days in appointments are not allowed."
                        }
                    );
                }

                var result = this.appointmentService.CreateNewAppointment(appointmentDto, doctorId);

                if (result != "Succeeded")
                    return StatusCode(
                        500,
                        new
                        {
                            success = false,
                            statusCode = 500,
                            messgae = result
                        }
                    );

                return Ok(
                    new
                    {
                        success = true,
                        statusCode = 201,
                        messgae = "Appointments created successfully.",
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
                        statusCOde = 500,
                        messgae = ex.Message
                    }
                );
            }
        }

        [HttpPatch("{appointmentTimeId:int}")]
        public IActionResult UpdateAppointmentTimeById(
            [FromBody] UpdateAppointmentTimeDto updateAppointmentTimeDto,
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

                string doctorId = decodedToken.Claims.First(claim => claim.Type == "UserId").Value;
                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Doctor")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with doctor account."
                        }
                    );
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            messgae = ModelState.ValidationState
                        }
                    );
                }

                var result = this.appointmentService.UpdateAppointmentTimeById(
                    updateAppointmentTimeDto,
                    appointmentTimeId,
                    doctorId
                );

                if (result != "Succeeded")
                    return StatusCode(
                        500,
                        new
                        {
                            success = false,
                            statusCode = 500,
                            messgae = result
                        }
                    );

                return Ok(
                    new
                    {
                        success = true,
                        statusCode = 201,
                        messgae = "Appointment updated successfully.",
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
                        statusCOde = 500,
                        messgae = ex.Message
                    }
                );
            }
        }

        [HttpDelete("{appointmentTimeId:int}")]
        public IActionResult DeleteAppointmentTimeById([FromRoute] int appointmentTimeId)
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

                string doctorId = decodedToken.Claims.First(claim => claim.Type == "UserId").Value;
                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Doctor")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with doctor account."
                        }
                    );
                }

                var result = this.appointmentService.DeleteAppointmentTimeById(
                    appointmentTimeId,
                    doctorId
                );

                if (result != "Succeeded")
                    return StatusCode(
                        500,
                        new
                        {
                            success = false,
                            statusCode = 500,
                            messgae = result
                        }
                    );

                return Ok(
                    new
                    {
                        success = true,
                        statusCode = 201,
                        messgae = "Appointment deleted successfully.",
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
                        statusCOde = 500,
                        messgae = ex.Message
                    }
                );
            }
        }
    }
}
