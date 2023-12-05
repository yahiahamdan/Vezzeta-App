using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            message = ModelState.ValidationState
                        }
                    );
                }
                else
                {
                    if (registerDto.Image != null)
                    {
                        if (
                            registerDto.Image.ContentType != "image/jpg"
                            && registerDto.Image.ContentType != "image/png"
                            && registerDto.Image.ContentType != "image/jpeg"
                        )
                            return BadRequest(
                                new
                                {
                                    success = false,
                                    statusCode = 400,
                                    message = "Invalid image type."
                                }
                            );

                        if (registerDto.Image.Length > 5 * 1024 * 1024)
                            return BadRequest(
                                new
                                {
                                    success = false,
                                    statusCode = 400,
                                    message = "Invalid image Size."
                                }
                            );
                    }

                    var user = await this.authService.Register(registerDto);

                    if (user.Succeeded)
                    {
                        return Created(
                            "User created successfully",
                            new
                            {
                                success = true,
                                statusCode = 201,
                                message = "User created successfully."
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(
                            500,
                            new
                            {
                                success = false,
                                statusCode = 500,
                                message = user.Errors
                            }
                        );
                    }
                }
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            message = ModelState.ValidationState
                        }
                    );
                }

                var user = await this.authService.Login(loginDto);

                if (user == null)
                {
                    return Unauthorized(
                        new
                        {
                            success = false,
                            statusCode = 401,
                            message = "Invalid credentials."
                        }
                    );
                }
                Console.WriteLine(user.LastName);
                Console.WriteLine(user.Email);
                Console.WriteLine(user.FirstName);
                return Created(
                    "User logged in successfully",
                    new
                    {
                        success = true,
                        statusCode = 201,
                        message = "User logged in successfully"
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
