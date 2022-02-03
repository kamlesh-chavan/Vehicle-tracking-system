using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using VehicleTrackingSystem.Bal.Services.VehicleDeviceService;
using VehicleTrackingSystem.Core.Constants;
using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Api.Controllers
{
    [Authorize]
    [Route(ApiRouteConstants.VehicleDevice)]
    [ApiController]
    public class VehicleDeviceController : ControllerBase
    {

        private readonly IVehicleDeviceService vehicleDeviceService;
        public VehicleDeviceController(IVehicleDeviceService vehicleDeviceService)
        {
            this.vehicleDeviceService = vehicleDeviceService;
        }

        /// <summary>
        /// Register new Vehicle and Device mapping
        /// In order to use new vehicle or device, you need to map them.
        /// </summary>
        /// <param name="model"> VehicleId and DeviceId</param>
        /// <returns>return success/failure/validation response</returns>
        [HttpPost]
        public async Task<IActionResult> AddVehicleDevice(VehicleDeviceInputModel model)
        {
            try
            {
                if (!IsValid(model))
                    throw new Exception(message: Messages.PleaseProvideRequiredValues);

                await this.vehicleDeviceService.AddVehicleDevice(model);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"{Messages.SomethingWentWrong}: {ex.Message}");
                return SendError(ex.Message);
            }
        }

        /// <summary>
        /// retirve a VehicleDevice by its Id
        /// </summary>
        /// <param name="vehicleDeviceId">vehicleDevice Id</param>
        /// <returns>return vehicle/failure response</returns>
        [HttpGet]
        public async Task<IActionResult> GetDevice(Guid vehicleDeviceId)
        {
            try
            {
                return Ok(await this.vehicleDeviceService.GetVehicleDeviceById(vehicleDeviceId));
            }
            catch (Exception ex)
            {
                Log.Error($"{Messages.SomethingWentWrong}: {ex.Message}");
                return SendError(ex.Message);
            }

        }

        /// <summary>
        /// retirve all VehicleDevices
        /// </summary>
        /// <returns>return all devices/failure response</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllVehicleDevice()
        {
            try
            {
                return Ok(await this.vehicleDeviceService.GetAllVehicleDevice());
            }
            catch (Exception ex)
            {
                Log.Error($"{Messages.SomethingWentWrong}: {ex.Message}");
                return SendError(ex.Message);
            }

        }


        private bool IsValid(VehicleDeviceInputModel model)
        {
            return !(model == null || model?.VehicleId == null || model?.DeviceId == null);
        }

        private IActionResult SendError(string message)
        {
            return !string.IsNullOrEmpty(message) ? BadRequest($"{message}") : BadRequest($"{Messages.SomethingWentWrong}");
        }
    }
}
