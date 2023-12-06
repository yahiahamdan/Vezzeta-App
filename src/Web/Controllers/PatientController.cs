using Application.Interfaces.Helpers;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IJwtHelpService jwtHelpService;

        public PatientController(
            IPatientService patientService,
            IHttpContextAccessor httpContextAccessor,
            IJwtHelpService jwtHelpService
        )
        {
            this.httpContextAccessor = httpContextAccessor;
            this.patientService = patientService;
            this.jwtHelpService = jwtHelpService;
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCountOfPatients()
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

                int numberOfPatients = await this.patientService.GetCountOfPatients("Patient");

                return Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
                        numberOfPatients
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
                        message = ex
                    }
                );
            }
        }
    }
}
