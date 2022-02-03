using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using VehicleTrackingSystem.Bal;
using VehicleTrackingSystem.Bal.Services.AuthService;
using VehicleTrackingSystem.Core.Constants;
using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Api.Controllers
{
    [Route(ApiRouteConstants.Token)]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticateService authenticateService;
        public TokenController(IAuthenticateService authenticateService)
        {
            this.authenticateService = authenticateService;
        }

        /// <summary>
        ///  Enpoint is to generate bearer token based on given email and password.
        ///  Currently the token is valid for 1 day. Validity in minute time can be configured in appSettings.json.
        /// </summary>
        /// <param name="model">
        /// Valid Email and Password required
        /// </param>
        /// <returns>Return a user detail along with a bearer token</returns>
        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            try
            {
                return Ok(await this.authenticateService.Authenticate(model));
            }
            catch (Exception ex)
            {
                Log.Error($"{Messages.SomethingWentWrong}: {ex.Message}");
                return SendError(ex.Message);
            }
        }



        private IActionResult SendError(string message)
        {
            return !string.IsNullOrEmpty(message) ? BadRequest($"{message}") : BadRequest($"{Messages.SomethingWentWrong}");
        }


    }
}
