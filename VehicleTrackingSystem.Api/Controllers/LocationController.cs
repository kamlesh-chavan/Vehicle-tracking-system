using Microsoft.AspNetCore.Mvc;
using Serilog;
using VehicleTrackingSystem.Core.Helper;
using VehicleTrackingSystem.Bal.Services.LocationService;
using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Core.Constants;
using Microsoft.AspNetCore.Authorization;

namespace VehicleTrackingSystem.Api.Controllers
{
    [Authorize]
    [Route(ApiRouteConstants.Location)]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService locationService;
        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        /// <summary>
        /// Record location from vehicles
        /// </summary>
        /// <param name="model">
        /// VehicleId and DeviceId are mapped to each other in db. Only valid mapped input will create location data in db
        /// Details is optional
        /// </param>
        /// <returns>return success/failure/validation response</returns>
        [HttpPost]
        public async Task<IActionResult> AddLocation(LocationInputModel model)
        {
            try
            {
                if (!ValidationHelper.IsLocationInputValid(model))
                    throw new Exception(message: Messages.PleaseProvideRequiredValues);

                await this.locationService.AddLocation(model);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"{Messages.SomethingWentWrong}: {ex.Message}");
                return SendError(ex.Message);
            }
        }

        /// <summary>
        /// Retirves location of vehicles
        /// This endpoint requires Authentication. 
        /// You can use api/token endpoint to generate bearer token using credentials mentioned in README.md file or add new user and its role in db and use.
        /// Only Admin role type users are allowed access to this endpoint
        /// </summary>
        /// <param name="model">
        /// - VehicleId and DeviceId are mandatory
        /// - Latitude/Longitude is optional. But if only 1 is provided it will return empty object
        /// - StartTime and EndTime is optional. When option send it as null 
        /// - GetCurrentLocation gives current location of vehicle if any (location within "cuurent time - 30 seconds")
        /// </param>
        /// <returns>
        /// - Return locations of give vehicle and device
        /// - If Latitude/Longitude is provide it will only return data for that Latitude/Longitude of given vehicle and device
        /// - If start/end time given then it provide data of vehicle during that given time 
        /// </returns>
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = UserRolesConstants.Admin)]
        //[Authorize(Roles = UserRolesConstants.Admin)]
        [HttpPost("get")]
        public async Task<IActionResult> GetLocation([FromBody]LocationSearchInputModel model)
        {
            try
            {
                if (!ValidationHelper.IsLocationSearchInputValid(model))
                    throw new Exception(message: Messages.PleaseProvideRequiredValues);

                return Ok(await this.locationService.GetLocations(model));
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
