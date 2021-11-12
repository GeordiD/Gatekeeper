using System.Linq;
using System.Threading.Tasks;
using Gatekeeper.Models.Api.Requests;
using Gatekeeper.Models.Api.Responses;
using Gatekeeper.Models.Responses.Authentication;
using Gatekeeper.Repos;
using Gatekeeper.Utilities.Config;

namespace Gatekeeper.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> AuthenticateUser(UserLoginRequest request);
        Task<TryCreateLoginResponse> TryCreateLogin(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private ILoginRepo _loginRepo;
        private IAuthConfig _authConfig;

        public AuthenticationService(ILoginRepo loginRepo, IAuthConfig authConfig)
        {
            _loginRepo = loginRepo;
            _authConfig = authConfig;
        }

        public async Task<LoginResponse> AuthenticateUser(UserLoginRequest request)
        {
            if (await _loginRepo.CheckLogin(request.Username, request.Password))
            {
                return new LoginResponse()
                {
                    Id = 420, //TODO
                    Username = request.Username,
                };
            }

            return null;
        }

        public async Task<TryCreateLoginResponse> TryCreateLogin(string username, string password)
        {
            var response = new TryCreateLoginResponse()
            {
                Username = await UsernameMeetsCriteria(username),
                Password = PasswordMeetsCriteria(password)
            };

            if (response.Success)
            {
                await _loginRepo.CreateLogin(username, password);
            }

            return response;
        }

        private async Task<CreateUsernameResponse> UsernameMeetsCriteria(string username)
        {
            return new CreateUsernameResponse()
            {
                MeetsMinLength = username.Length > 0 &&
                    username.Length >= _authConfig.Username.MinLength,
                Unique = !(await _loginRepo.UsernameExists(username))
            };
        }

        private CreatePasswordResponse PasswordMeetsCriteria(string password)
        {
            return new CreatePasswordResponse()
            {
                MeetsMinLength = password.Length > 0
                    && password.Length >= _authConfig.Password.MinLength,
                HasNumber = _authConfig.Password.RequireNumber ?
                    password.Any(char.IsDigit) : null,
                HasUppercase = _authConfig.Password.RequireUppercase ?
                    password.Any(char.IsUpper) : null,
                HasSymbol = _authConfig.Password.RequireSymbol ?
                    !password.All(char.IsLetterOrDigit) : null
            };
        }
    }
}