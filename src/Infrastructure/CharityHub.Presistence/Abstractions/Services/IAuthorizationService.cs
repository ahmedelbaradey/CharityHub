using CharityHub.Domain.Entities.Identities;
using CharityHub.Domain.Helpers;

namespace CharityHub.DomainService.Abstractions.Services
{
    public interface IAuthorizationService
    {
        public Task<bool> AddRoleAsync(string roleName);
        public Task<Role> GetRoleByID(int Id);
        public Task<List<Role>> GetRoleListAsync();
 
    }
}
