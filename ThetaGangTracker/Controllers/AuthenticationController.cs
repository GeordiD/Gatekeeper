using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThetaGangTracker.Models.ApiRequests;
using ThetaGangTracker.Models;
using ThetaGangTracker.Services;

namespace ThetaGangTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IJwtTokenService _jwtService;

        public AuthenticationController(IJwtTokenService jwtService)
        {
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginRequest request)
        {
            var user = CheckAuth(request);

            if (user == null)
                return Unauthorized();

            return Ok(new
            {
                token = _jwtService.GenerateJwt(user)
            });
        }

        // Very hard-coded version of authentication to test JWT setup. TBD
        private User CheckAuth(UserLoginRequest request)
        {
            if (request.Username == "GeordiD" && request.Password == "tacos")
            {
                return new User()
                {
                    Id = 420,
                    Username = "GeordiD",
                    Password = "not today satan",
                    Email = "geordi.dosher@gmail.com"
                };
            }

            return null;
        }
    }
}