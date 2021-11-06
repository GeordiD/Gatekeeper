using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThetaGangTracker.Models.Api.Requests
{
    public class CreateLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}