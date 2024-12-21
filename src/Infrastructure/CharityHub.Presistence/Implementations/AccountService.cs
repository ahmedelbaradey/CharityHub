using CharityHub.DomainService.Abstractions.Services;
using CharityHub.DomainService.Abstractions.Repository;
using CharityHub.Domain.Entities.Identities;
using CharityHub.DomainService.Abstractions.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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
        public async Task<bool> CreateAccountAsync(Account account)
        {
            try
            {
                var IsAdded = await _accountRepository.AddAsync(account);
                if (IsAdded == null)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in CreateClinicAsync");
                throw;
            }

        }

        public async Task<Account> GetAccountById(int accountId)
        {
            return await _accountRepository.GetByIdAsync(accountId);
        }
        public async Task<Account> GetAccountByMobileNumber(string mobileNumber)
        {
            return await _accountRepository.FindByCondition(c=>c.MobileNumber.Equals(mobileNumber),trackChanges : false).SingleOrDefaultAsync();
        }

        public   IQueryable<Account> GetAllAccounts()
        {
            return   _accountRepository.FindAll(trackChanges: false);
        }

        public async Task<bool> UpdateAccountAsync(Account account)
        {
            try
            {
                var result = await _accountRepository.UpdateAsync(account);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogDebug( "Error in UpdateAccountAsync");
                throw;
            }
        }
        #endregion
    }
}
