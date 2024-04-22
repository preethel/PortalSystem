using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalSystem.Api.Dto;
using PortalSystem.Api.Shared;
using PortalSystem.Service.UserServices;

namespace PortalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult<string> GetMe()
        {
            var userName = "Redwan";
            return Ok(userName);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<Guid>>> Register(RegistrationRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.CreateUser(new CreateUserRequest(request.Name, request.Email, request.Password));

                var res = new ServiceResponse<Guid>
                {
                    Data = user.Id,
                    Message = "Registration succeed.",
                    Success = true
                };
                return Ok(res);


            }
            
            return BadRequest("Try with valid information.");

        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest("Try with valid information."); }
            var result = await _userService.LoginUser(new LoginUserRequest(request.Email, request.Password));
            //SetRefreshToken(result.RefreshToken);
            return Ok(new ServiceResponse<string>
            {
                Data = result.AccessToken,
                Message = "Login succeed.",
                Success = true
            });
        }

        [HttpGet("refresh-token")]
        public async Task<ActionResult> RefreshToken(string refreshToken)
        {
            var result = await _userService.RefreshToken(refreshToken);
            //SetRefreshToken(result.RefreshToken);
            return Ok(result.AccessToken);
        }
        [HttpPost("logout")]
        public async Task<ActionResult> LogOut(string refreshToken)
        {
            if (refreshToken != null)
            {
                await _userService.RemoveRefreshToken(refreshToken);
            }
            return Ok("Logged out successfully");
        }

    }
}
