using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Bal.Services.AuthService
{
    public  interface IAuthenticateService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    }
}
