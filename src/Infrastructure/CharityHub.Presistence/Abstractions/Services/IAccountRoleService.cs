using CharityHub.Domain.Entities;
using CharityHub.Domain.Entities.Identities;

namespace CharityHub.DomainService.Abstractions.Services
{
    public interface IAccountRolesService
    {
        public Task<IReadOnlyList<AccountRole>> GetAccountRolesAsync(int accountId);

        public Task<bool> AddToRoleAsync(Role role,Account account);


    }
}
