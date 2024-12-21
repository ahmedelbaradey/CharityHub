using CharityHub.Application.Services.Accounts.Commands.Models;
using CharityHub.Domain.Entities.Identities;

namespace CharityHub.Application.Mapping.Accounts
{
    public partial class AccountProfile
    {
        public void EditAccountMapping()
        {
            CreateMap<EditAccountCommand, Account>();
        }
    }
}
