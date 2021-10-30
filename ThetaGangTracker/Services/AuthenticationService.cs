using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThetaGangTracker.Models;
using ThetaGangTracker.Models.ApiRequests;
using ThetaGangTracker.Repos;

namespace ThetaGangTracker.Services
{
    public interface IAuthenticationService
    {
        Task<User> AuthenticateUser(UserLoginRequest request);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private ILoginRepo _loginRepo;

        public AuthenticationService(ILoginRepo loginRepo)
        {
            _loginRepo = loginRepo;
        }

        public async Task<User> AuthenticateUser(UserLoginRequest request)
        {
            if (await _loginRepo.CheckLogin(request.Username, request.Password))
            {
                // Users service coming soon to actually grab this data
                return new User()
                {
                    Id = 420,
                    Username = "GeordiD",
                };
            }

            return null;
        }
    }
}