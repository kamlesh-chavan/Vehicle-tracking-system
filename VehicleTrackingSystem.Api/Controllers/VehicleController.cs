using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using VehicleTrackingSystem.Bal.Services.VehicleService;
using VehicleTrackingSystem.Core.Constants;
using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Api.Controllers
{
    [Authorize]
    [Route(ApiRouteConstants.Vehicle)]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        /// <summary>
        /// Register new vehicle
        /// In order to use this new vehicle, you need to creat new device and assign it to vehicle in db.
        /// </summary>
        /// <param name="model"> Different details of vehicle</param>
        /// <returns>return success/failure/validation response</returns>
        [HttpPost]
        public async Task<IActionResult> AddVehile(VehicleInputModel model)
        {
            try
            {
                if (!IsValid(model))
                    throw new Exception(message: Messages.PleaseProvideRequiredValues);

                await this.vehicleService.AddVehicle(model);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"{Messages.SomethingWentWrong}: {ex.Message}");
                return SendError(ex.Message);
            }
        }

        /// <summary>
        /// retirve a vehicle by its Id
        /// </summary>
        /// <param name="vehicleDeviceId">vehicle Id</param>
        /// <returns>return vehicle/failure response</returns>
        [HttpGet]
        public async Task<IActionResult> GetVehicle(Guid vehicleId)
        {
            try
            {
                return Ok(await this.vehicleService.GetVehicle(vehicleId));
            }
            catch (Exception ex)
            {
                Log.Error($"{Messages.SomethingWentWrong}: {ex.Message}");
                return SendError(ex.Message);
            }

        }

        /// <summary>
        /// retirve all vehicles
        /// </summary>
        /// <returns>returns  vehicles/failure response</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllVehicles()
        {
            try
            {
                return Ok(await this.vehicleService.GetAllVehicles());
            }
            catch (Exception ex)
            {
                Log.Error($"{Messages.SomethingWentWrong}: {ex.Message}");
                return SendError(ex.Message);
            }

        }


        private bool IsValid(VehicleInputModel model)
        {
            return !(model == null || string.IsNullOrEmpty(model.Model) || string.IsNullOrEmpty(model.ModelNumber) || string.IsNullOrEmpty(model.Maker));
        }

        private IActionResult SendError(string message)
        {
            return !string.IsNullOrEmpty(message) ? BadRequest($"{message}") : BadRequest($"{Messages.SomethingWentWrong}");
        }

    }
}
