using CharityHub.Application.Base;
using CharityHub.Domain.Helpers;
using MediatR;

namespace CharityHub.Application.Services.Authorizations.Queries.Handlers.ManageUserRoles
{
    public class ManageUserRolesQuery : IQuery<BaseResponse<ManageUserRolesResponse>>
    {
        public int Id { get; set; }
    }
}
