using CharityHub.DomainService.Abstractions.Services;
using CharityHub.DomainService.Abstractions.Repository;
using CharityHub.Domain.Entities.Identities;
using CharityHub.DomainService.Abstractions.Logger;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.DomainService.Implementations
{
    public class AccountRoleService : IAccountRolesService
    {

        #region Fileds
        private readonly ILoggerManager _logger;
        private readonly IGenericRepository<AccountRole> _accountRoleRepository;
 
        #endregion

        #region Constructors
        public AccountRoleService(IGenericRepository<AccountRole> accountRoleRepository, ILoggerManager logger)
        {
            _logger = logger;
            _accountRoleRepository = accountRoleRepository;
        }
        #endregion

        #region Handle Functions


        public async Task<bool> CreateAccountRoleAsync(AccountRole accountRole)
        {
            try
            {
                var result = await _accountRoleRepository.AddAsync(accountRole);
                if (result == null)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in CreateAccountRoleAsync");
                throw;
            }

        }

        public async Task<IReadOnlyList<AccountRole>> GetAccountRolesAsync(int accountId)
        {
            try
            {
                return await _accountRoleRepository.FindByCondition(c => c.AccountId.Equals(accountId), trackChanges: false).ToListAsync();
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
