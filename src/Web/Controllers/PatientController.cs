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

        public PatientController(
            IPatientService patientService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            this.httpContextAccessor = httpContextAccessor;
            this.patientService = patientService;
        }

        [HttpGet("count")]
        /*[Authorize]*/
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

                int numberOfPatients = await this.patientService.GetCountOfPatients(accessToken);

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
                        message = ex.Message
                    }
                );
            }
        }
    }
}
