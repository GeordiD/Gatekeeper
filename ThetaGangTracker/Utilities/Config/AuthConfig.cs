using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ThetaGangTracker.Utilities.Config
{
    public interface IAuthConfig
    {
        AuthUsernameConfig Username { get; }
        AuthPasswordConfig Password { get; }
    }

    public class AuthConfig : IAuthConfig
    {
        private IConfiguration _config;

        public AuthConfig(IConfiguration config)
        {
            _config = config;
        }

        public AuthUsernameConfig Username
        {
            get => _config.GetSection("AuthSettings:Username").Get<AuthUsernameConfig>();
        }
        public AuthPasswordConfig Password
        {
            get => _config.GetSection("AuthSettings:Password").Get<AuthPasswordConfig>();
        }
    }

    public interface IAuthUsernameConfig
    {
        int MinLength { get; set; }
    }

    public class AuthUsernameConfig : IAuthUsernameConfig
    {
        public int MinLength { get; set; }
    }

    public interface IAuthPasswordConfig
    {
        int MinLength { get; set; }
        bool RequireUppercase { get; set; }
        bool RequireNumber { get; set; }
        bool RequireSymbol { get; set; }
    }

    public class AuthPasswordConfig : IAuthPasswordConfig
    {
        public int MinLength { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireNumber { get; set; }
        public bool RequireSymbol { get; set; }
    }
}