using CharityHub.DomainService.Abstractions.Services;
using CharityHub.DomainService.Abstractions.Repository;
using CharityHub.Domain.Entities.Identities;
using CharityHub.DomainService.Abstractions.Logger;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.DomainService.Implementations
{
    public class AccountService : IAccountService
    {
        #region Fileds
        private readonly ILoggerManager _logger;
        private readonly IGenericRepository<Account> _accountRepository;
        #endregion

        #region Constructors
        public AccountService(ILoggerManager logger, IGenericRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
            _logger = logger;

        }
        #endregion

        #region Functions
        public async Task<Account> GetAccountById(int accountId)
        {
            return await _accountRepository.GetByIdAsync(accountId);
        }
        public async Task<Account> GetAccountByMobileNumber(string mobileNumber)
        {
            return await _accountRepository.FindByCondition(c=>c.MobileNumber.Equals(mobileNumber),trackChanges : false).SingleOrDefaultAsync();
        }
        #endregion
    }
}
