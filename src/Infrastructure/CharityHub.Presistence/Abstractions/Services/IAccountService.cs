using CharityHub.Domain.Entities;
using CharityHub.Domain.Entities.Identities;

namespace CharityHub.DomainService.Abstractions.Services
{
    public interface IAccountService
    {
       Task<Account> GetAccountById(int accountId);
        Task<Account> GetAccountByMobileNumber(string mobileNumber);
        Task<bool> CreateAccountAsync(Account account);
        Task<bool> UpdateAccountAsync(Account account);
        IQueryable<Account> GetAllAccounts();
    }
}
