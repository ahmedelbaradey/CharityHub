using CharityHub.DomainService.Abstractions.Services;
using CharityHub.DomainService.Abstractions.Repository;
using CharityHub.Domain.Entities.Identities;
using CharityHub.DomainService.Abstractions.Logger;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.DomainService.Implementations
{
    public class RoleService : IRoleService
    {

        #region Fileds
        private readonly ILoggerManager _logger;
        private readonly IGenericRepository<Role> _roleRepository;
 
        #endregion

        #region Constructors
        public RoleService(IGenericRepository<Role> roleRepository, ILoggerManager logger)
        {
            _logger = logger;
            _roleRepository = roleRepository;
        }
        #endregion

        #region Handle Functions


        public async Task<bool> CreateRoleAsync(Role role)
        {
            try
            {
                var result = await _roleRepository.AddAsync(role);
                if (result == null)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in CreateRoleAsync");
                throw;
            }

        }

        public async Task<IReadOnlyList<Role>> GetAccountRolesAsync(int accountId)
        {
            try
            {
                return await _roleRepository.FindByCondition(c => c.AccountRoles.Any(x => x.AccountId.Equals(accountId)), trackChanges:false).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in CreateAccountRoleAsync");
                throw;
            }
        }
        #endregion
    }
}
