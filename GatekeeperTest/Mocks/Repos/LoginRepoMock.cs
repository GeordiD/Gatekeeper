using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gatekeeper.Models.Responses.Authentication;
using Gatekeeper.Repos;
using Moq;

namespace GatekeeperTest.Mocks.Repos
{
    public static class LoginRepoMock
    {
        public static Mock<ILoginRepo> Mock(bool? checkLoginResult, bool? usernameExists)
        {
            var mock = new Mock<ILoginRepo>();
            mock
                .Setup(x => x.CreateLogin(It.IsAny<string>(), It.IsAny<string>()));

            if (checkLoginResult != null)
                mock
                    .Setup(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(checkLoginResult.Value);

            if (usernameExists != null)
                mock
                    .Setup(x => x.UsernameExists(It.IsAny<string>()))
                    .ReturnsAsync(usernameExists.Value);

            return mock;
        }
    }
}