using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gatekeeper.Utilities.Config;
using Moq;

namespace GatekeeperTest.Mocks.Configs
{
    public class AuthConfigBuilder
    {
        public AuthUsernameConfig Username = new AuthUsernameConfig();
        public AuthPasswordConfig Password = new AuthPasswordConfig();

        public AuthConfigBuilder()
        {
            Reset();
        }

        public IAuthConfig Get()
        {
            var mock = new Mock<IAuthConfig>();
            mock
                .Setup(x => x.Username)
                .Returns(Username);
            mock
                .Setup(x => x.Password)
                .Returns(Password);
            return mock.Object;
        }

        public void Reset()
        {
            Username = new AuthUsernameConfig();
            Password = new AuthPasswordConfig();
        }
    }
}