using CharityHub.Domain.Entities;
using CharityHub.Domain.Entities.Identities;
using CharityHub.DomainService.Abstractions.Repository;

namespace CharityHub.DomainService.Abstractions.Services
{
    public interface IRoleService 
    {
        Task<Role> GetRoleByNameAsync(string roleName);
       
    }
}
