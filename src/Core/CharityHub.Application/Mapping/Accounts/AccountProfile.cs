using AutoMapper;

namespace CharityHub.Application.Mapping.Accounts
{
    public partial class AccountProfile : Profile
    {
        public AccountProfile()
        {
            AddAccountMapping();
            EditAccountMapping();
            GetAccountsPaginatedListMapping();
            GetAccountByIdMapping();
        }
    }
}
