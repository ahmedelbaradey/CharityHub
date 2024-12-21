using CharityHub.DomainService.Abstractions.Services;
using CharityHub.DomainService.Abstractions.Repository;
using CharityHub.Domain.Entities.Identities;
using CharityHub.DomainService.Abstractions.Logger;

namespace CharityHub.DomainService.Implementations
{
    public class DeviceService : IDeviceService
    {

        #region Fileds
        private readonly ILoggerManager _logger;
        private readonly IGenericRepository<Device> _deviceRepository;
 
        #endregion

        #region Constructors
        public DeviceService(IGenericRepository<Device> deviceRepository , ILoggerManager logger)
        {
            _logger = logger;
            _deviceRepository = deviceRepository;
        }
        #endregion

        #region Handle Functions


        public async Task<bool> CreateDeviceAsync(Device device)
        {
            try
            {
                var result = await _deviceRepository.AddAsync(device);
                if (result == null)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in CreateDeviceAsync");
                throw;
            }

        }

        public async Task<bool> DeleteDeviceAsync(Device device)
        {
            try
            {
                var result = await _deviceRepository.DeleteAsync(device);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in DeleteDeviceAsync");
                throw;
            }
        }

      
        public async Task<bool> UpdateDeviceAsync(Device device)
        {
            try
            {
                var result = await _deviceRepository.UpdateAsync(device);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in UpdateDeviceAsync");
                throw;
            }
        }
        #endregion
    }
}
