using CharityHub.Application.Services.Accounts.Queries.Responses;
using CharityHub.Domain.Entities.Identities;

namespace CharityHub.Application.Mapping.Accounts
{
    public partial class AccountProfile
    {
        public void GetAccountsPaginatedListMapping()
        {
            CreateMap<Account, GetAccountPaginatedListResponse>();
        }
    }
}
