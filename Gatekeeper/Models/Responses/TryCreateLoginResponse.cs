using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gatekeeper.Models.Responses.Authentication
{
    public class TryCreateLoginResponse
    {
        public bool Success
        {
            get
            {
                return Username.Success && Password.Success;
            }
        }
        public CreateUsernameResponse Username { get; set; }
        public CreatePasswordResponse Password { get; set; }
    }

    public class CreateUsernameResponse
    {
        public bool Success
        {
            get
            {
                return MeetsMinLength && Unique;
            }
        }
        public bool MeetsMinLength { get; set; }
        public bool Unique { get; set; }
    }

    public class CreatePasswordResponse
    {
        public bool Success
        {
            get
            {
                return MeetsMinLength && HasNumber && HasSymbol && HasUppercase;
            }
        }

        public bool MeetsMinLength { get; set; }
        public bool HasNumber { get; set; }
        public bool HasUppercase { get; set; }
        public bool HasSymbol { get; set; }
    }
}