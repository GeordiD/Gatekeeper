using System;
using System.Threading.Tasks;
using Gatekeeper.Models.Api.Requests;
using Gatekeeper.Models.Responses.Authentication;
using Gatekeeper.Repos;
using Gatekeeper.Services;
using Gatekeeper.Utilities.Config;
using GatekeeperTest.Mocks.Configs;
using GatekeeperTest.Mocks.Repos;
using Moq;
using Xunit;

namespace GatekeeperTest.Services
{
    public class AuthenticationServiceTests
    {
        #region AuthenticateUser()

        [Fact]
        public async Task AuthenticateUser_ShouldReturnNull_WhenCheckLoginReturnsFalse()
        {
            // Arrange
            var loginRepo = LoginRepoMock.Mock(false, null);
            var authConfig = new Mock<IAuthConfig>();
            var service = new AuthenticationService(loginRepo.Object, authConfig.Object);
            var request = new UserLoginRequest()
            {
                Username = "TestUser",
                Password = "Password"
            };

            // Act
            var result = await service.AuthenticateUser(request);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnUserInfo_WhenCheckLoginReturnsTrue()
        {
            // Arrange
            var loginRepo = LoginRepoMock.Mock(true, null);
            var authConfig = new Mock<IAuthConfig>();
            var service = new AuthenticationService(loginRepo.Object, authConfig.Object);
            var request = new UserLoginRequest()
            {
                Username = "TestUser",
                Password = "Password"
            };

            // Act
            var result = await service.AuthenticateUser(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestUser", result.Username);
        }

        #endregion

        #region TryCreateLogin()

        [Theory]
        [InlineData(4, "xxx", false)]
        [InlineData(0, "", false)]
        [InlineData(4, "xxxxx", true)]
        [InlineData(4, "xxxx", true)]
        [InlineData(0, "x", true)]
        public async Task TryCreateLogin_ShouldCheckUsernameMinLengthRequirements_Always(int minLength, string username, bool expected)
        {
            // Arrange
            var configBuilder = new AuthConfigBuilder();
            configBuilder.Username.MinLength = minLength;

            var loginRepo = LoginRepoMock.Mock(false, false);
            var service = new AuthenticationService(loginRepo.Object, configBuilder.Get());

            // Act
            var result = await service.TryCreateLogin(username, "");

            // Assert
            Assert.Equal(expected, result.Username.MeetsMinLength);
        }

        [Theory]
        [InlineData(4, "xxx", false)]
        [InlineData(0, "", false)]
        [InlineData(4, "xxxxx", true)]
        [InlineData(4, "xxxx", true)]
        [InlineData(0, "x", true)]
        public async Task TryCreateLogin_ShouldCheckPasswordMinLength_Always(int minLength, string password, bool expected)
        {
            // Arrange
            var configBuilder = new AuthConfigBuilder();
            configBuilder.Password.MinLength = minLength;

            var loginRepo = LoginRepoMock.Mock(false, false);
            var service = new AuthenticationService(loginRepo.Object, configBuilder.Get());

            // Act
            var result = await service.TryCreateLogin("", password);

            // Assert
            Assert.Equal(expected, result.Password.MeetsMinLength);
        }

        [Theory]
        [InlineData("")]
        [InlineData("4")]
        [InlineData("#")]
        [InlineData("One")]
        public async Task TryCreateLogin_ShouldReturnNullForPasswordHasNumber_WhenConfiguredToNotCheckForNumber(string password)
        {
            // Arrange
            var configBuilder = new AuthConfigBuilder();
            configBuilder.Password.RequireNumber = false;

            var loginRepo = LoginRepoMock.Mock(false, false);
            var service = new AuthenticationService(loginRepo.Object, configBuilder.Get());

            // Act
            var result = await service.TryCreateLogin("", password);

            // Assert
            Assert.Null(result.Password.HasNumber);
        }

        [Theory]
        [InlineData("4", true)]
        [InlineData("1Square", true)]
        [InlineData("numBer0", true)]
        [InlineData("", false)]
        [InlineData("one", false)]
        [InlineData("#G(&@^!)", false)]
        public async Task TryCreateLogin_ShouldCheckPasswordHasNumber_WhenConfiguredToCheckForNumber(string password, bool expected)
        {
            // Arrange
            var configBuilder = new AuthConfigBuilder();
            configBuilder.Password.RequireNumber = true;

            var loginRepo = LoginRepoMock.Mock(false, false);
            var service = new AuthenticationService(loginRepo.Object, configBuilder.Get());

            // Act
            var result = await service.TryCreateLogin("", password);

            // Assert
            Assert.Equal(expected, result.Password.HasNumber);
        }

        [Theory]
        [InlineData("")]
        [InlineData("4")]
        [InlineData("#")]
        [InlineData("One")]
        public async Task TryCreateLogin_ShouldReturnNullForPasswordHasSymbol_WhenConfiguredToNotCheckForSymbol(string password)
        {
            // Arrange
            var configBuilder = new AuthConfigBuilder();
            configBuilder.Password.RequireSymbol = false;

            var loginRepo = LoginRepoMock.Mock(false, false);
            var service = new AuthenticationService(loginRepo.Object, configBuilder.Get());

            // Act
            var result = await service.TryCreateLogin("", password);

            // Assert
            Assert.Null(result.Password.HasSymbol);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("symbol", false)]
        [InlineData("4Square", false)]
        [InlineData("!", true)]
        [InlineData("@", true)]
        [InlineData("#", true)]
        [InlineData("$", true)]
        [InlineData("%", true)]
        [InlineData("^", true)]
        [InlineData("&", true)]
        [InlineData("*", true)]
        [InlineData("(", true)]
        [InlineData(")", true)]
        [InlineData("?", true)]
        [InlineData("/", true)]
        [InlineData("\"", true)]
        [InlineData("'", true)]
        [InlineData("-", true)]
        [InlineData("_", true)]
        [InlineData("=", true)]
        [InlineData("+", true)]
        [InlineData(" ", true)]
        [InlineData("Testing!", true)]
        public async Task TryCreateLogin_ShouldCheckPasswordHasSymbol_WhenConfiguredToCheckForSymbol(string password, bool expected)
        {
            // Arrange
            var configBuilder = new AuthConfigBuilder();
            configBuilder.Password.RequireSymbol = true;

            var loginRepo = LoginRepoMock.Mock(false, false);
            var service = new AuthenticationService(loginRepo.Object, configBuilder.Get());

            // Act
            var result = await service.TryCreateLogin("", password);

            // Assert
            Assert.Equal(expected, result.Password.HasSymbol);
        }

        [Theory]
        [InlineData("")]
        [InlineData("4")]
        [InlineData("#")]
        [InlineData("One")]
        public async Task TryCreateLogin_ShouldReturnNullForPasswordHasUppercase_WhenConfiguredToNotCheckForUppercase(string password)
        {
            // Arrange
            var configBuilder = new AuthConfigBuilder();
            configBuilder.Password.RequireUppercase = false;

            var loginRepo = LoginRepoMock.Mock(false, false);
            var service = new AuthenticationService(loginRepo.Object, configBuilder.Get());

            // Act
            var result = await service.TryCreateLogin("", password);

            // Assert
            Assert.Null(result.Password.HasUppercase);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("lowercase", false)]
        [InlineData("4", false)]
        [InlineData("$@(!$&@(!_+_-=", false)]
        [InlineData("Uppercase", true)]
        [InlineData("wHat", true)]
        [InlineData("YELLING", true)]
        public async Task TryCreateLogin_ShouldCheckPasswordHasUppercase_WhenConfiguredToCheckForUppercase(string password, bool expected)
        {
            // Arrange
            var configBuilder = new AuthConfigBuilder();
            configBuilder.Password.RequireUppercase = true;

            var loginRepo = LoginRepoMock.Mock(false, false);
            var service = new AuthenticationService(loginRepo.Object, configBuilder.Get());

            // Act
            var result = await service.TryCreateLogin("", password);

            // Assert
            Assert.Equal(expected, result.Password.HasUppercase);
        }

        // === Helpers ===


        #endregion
    }
}
