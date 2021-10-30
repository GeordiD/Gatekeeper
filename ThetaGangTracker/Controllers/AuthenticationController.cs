using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThetaGangTracker.Models.ApiRequests;
using ThetaGangTracker.Models;
using ThetaGangTracker.Services;
using System.Threading.Tasks;

namespace ThetaGangTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IJwtTokenService _jwtService;
        private IAuthenticationService _authService;

        public AuthenticationController(IJwtTokenService jwtService, IAuthenticationService authService)
        {
            _jwtService = jwtService;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var user = await _authService.AuthenticateUser(request);

            if (user == null)
                return Unauthorized();

            return Ok(new
            {
                token = _jwtService.GenerateJwt(user)
            });
        }
    }
}