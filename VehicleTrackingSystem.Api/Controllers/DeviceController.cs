using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using VehicleTrackingSystem.Bal.Services.DeviceService;
using VehicleTrackingSystem.Core.Constants;
using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Api.Controllers
{
    [Authorize]
    [Route(ApiRouteConstants.Device)]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly IDeviceService deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        /// <summary>
        /// Register new device
        /// In order to use this new device, you need to assign vehicle to it.
        /// </summary>
        /// <param name="model"> Different details of vehicle</param>
        /// <returns>return success/failure/validation response</returns>
        [HttpPost]
        public async Task<IActionResult> AddDevice(DeviceInputModel model)
        {
            try
            {
                if (!IsValid(model))
                    throw new Exception(message: Messages.PleaseProvideRequiredValues);

                await this.deviceService.AddDevice(model);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"{Messages.SomethingWentWrong}: {ex.Message}");
                return SendError(ex.Message);
            }
        }

        /// <summary>
        /// retirve a Device by its Id
        /// </summary>
        /// <param name="vehicleDeviceId">vehicle Id</param>
        /// <returns>return vehicle/failure response</returns>
        [HttpGet]
        public async Task<IActionResult> GetDevice(Guid deviceId)
        {
            try
            {
                return Ok(await this.deviceService.GetDevice(deviceId));
            }
            catch (Exception ex)
            {
                Log.Error($"{Messages.SomethingWentWrong}: {ex.Message}");
                return SendError(ex.Message);
            }

        }

        /// <summary>
        /// retirve all devices
        /// </summary>
        /// <returns>return all devices/failure response</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllDevices()
        {
            try
            {
                return Ok(await this.deviceService.GetAllDevices());
            }
            catch (Exception ex)
            {
                Log.Error($"{Messages.SomethingWentWrong}: {ex.Message}");
                return SendError(ex.Message);
            }

        }


        private bool IsValid(DeviceInputModel model)
        {
            return !(model == null || string.IsNullOrEmpty(model.Brand) || string.IsNullOrEmpty(model.Name) 
                || string.IsNullOrEmpty(model.Description) || string.IsNullOrEmpty(model.DeviceNumber));
        }

        private IActionResult SendError(string message)
        {
            return !string.IsNullOrEmpty(message) ? BadRequest($"{message}") : BadRequest($"{Messages.SomethingWentWrong}");
        }
    }
}
