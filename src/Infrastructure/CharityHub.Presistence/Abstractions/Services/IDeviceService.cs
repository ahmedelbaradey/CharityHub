using CharityHub.Domain.Entities;
using CharityHub.Domain.Entities.Identities;
using CharityHub.DomainService.Abstractions.Repository;

namespace CharityHub.DomainService.Abstractions.Services
{
    public interface IDeviceService
    {
        public Task<bool> CreateDeviceAsync(Device device);
        public Task<bool> UpdateDeviceAsync(Device device);
        public Task<bool> DeleteDeviceAsync(Device device);
    }
}
